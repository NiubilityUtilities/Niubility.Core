using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Niubility.Common.Web
{
    public abstract class EventStreamActionResult : ActionResult
    {
        private readonly IEventStreamHandler Handler;

        private static Lazy<byte[]> LazyStopEventContent;
        private byte[] StopEventContent { get => LazyStopEventContent.Value; }

        public EventStreamActionResult(IEventStreamHandler handler)
        {
            Handler = handler;
            if (null == LazyStopEventContent)
            {
                LazyStopEventContent = new Lazy<byte[]>(GenerateStopEventContent);
            }
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "text/event-stream";
            context.HttpContext.Response.Headers.CacheControl = "no-cache";
            context.HttpContext.Response.Headers.Connection = "keep-alive";

            var cancellation = context.HttpContext.RequestAborted;
            var writer = context.HttpContext.Response.BodyWriter;
            await writer.FlushAsync(cancellation);

            while (!Handler.IsCompleted)
            {
                var data = await Handler.Next();
                if (null != data)
                {
                    await writer.WriteAsync(GenerateEventContent(data));
                    await writer.FlushAsync(cancellation);
                }
            }
            await writer.WriteAsync(StopEventContent);
            await writer.FlushAsync(cancellation);
            await writer.CompleteAsync();
        }

        protected virtual byte[] GenerateEventContent(string data)
        {
            return Encoding.UTF8.GetBytes($"data: {data}\r\r");
        }
        protected virtual byte[] GenerateStopEventContent()
        {
            return Encoding.UTF8.GetBytes("data:over\r\r");
        }
    }
}