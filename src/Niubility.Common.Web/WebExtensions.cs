using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Niubility.Common.Web
{
    public static class WebExtensions
    {
        #region Wrap as ResponseMessage Result ...
        public static IActionResult WrapResponseMessage<T>(ResponseMessage<T> message)
        {
            switch (message.HttpStatusCode)
            {
                case 200:
                    return new JsonResult(message);
                case 401:
                    return new UnauthorizedResult();
                case 403:
                    return new ForbidResult();
                case 404:
                case 500:
                default:
                    return new ContentResult
                    {
                        StatusCode = message.HttpStatusCode,
                        Content = JsonConvert.SerializeObject(message),
                        ContentType = "application/json"
                    };
            }
        }



        public static async ValueTask<IActionResult> WrapResponseMessageAsync<T>(
            Func<ValueTask<ResponseMessage<T>>> dataDelegate,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            var message = await ResponseExtensions.WrapResponseMessageAsync(dataDelegate, logger);
            var result = (wrap ?? WrapResponseMessage)(message);
            return result;
        }
        public static ValueTask<IActionResult> WrapResponseMessageAsync<T, T1>(
            Func<T1, ValueTask<ResponseMessage<T>>> dataDelegate,
            T1 arg1,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            return WrapResponseMessageAsync(() => dataDelegate(arg1), logger, wrap);
        }
        public static ValueTask<IActionResult> WrapResponseMessageAsync<T, T1, T2>(
            Func<T1, T2, ValueTask<ResponseMessage<T>>> dataDelegate,
            T1 arg1, T2 arg2,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            return WrapResponseMessageAsync(() => dataDelegate(arg1, arg2), logger, wrap);
        }



        public static IActionResult WrapAsResponseMessageResult<T>(T data)
        {
            var message = ResponseExtensions.WrapAsResponseMessage(data);
            var result = WrapResponseMessage(message);
            return result;
        }

        public static IActionResult WrapAsResponseMessageResult<T>(Func<T> dataDelegate, ILogger logger)
        {
            var message = ResponseExtensions.WrapAsResponseMessage(dataDelegate, logger);
            var result = WrapResponseMessage(message);
            return result;
        }
        public static IActionResult WrapAsResponseMessageResult<T1, T>(Func<T1, T> dataDelegate,
            T1 arg1,
            ILogger logger)
        {
            return WrapAsResponseMessageResult(() => dataDelegate(arg1), logger);
        }
        public static IActionResult WrapAsResponseMessageResult<T1, T2, T>(Func<T1, T2, T> dataDelegate,
            T1 arg1, T2 arg2,
            ILogger logger)
        {
            return WrapAsResponseMessageResult(() => dataDelegate(arg1, arg2), logger);
        }

        public static async ValueTask<IActionResult> WrapAsResponseMessageResultAsync(Func<ValueTask> dataDelegate,
            ILogger logger)
        {
            var message = await ResponseExtensions.WrapAsResponseMessageAsync(dataDelegate, logger);
            var result = WrapResponseMessage(message);
            return result;
        }
        public static ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T1>(Func<T1, ValueTask> dataDelegate,
            T1 arg1,
            ILogger logger)
        {
            return WrapAsResponseMessageResultAsync(() => dataDelegate(arg1), logger);
        }
        public static ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T1, T2>(Func<T1, T2, ValueTask> dataDelegate,
            T1 arg1, T2 arg2,
            ILogger logger)
        {
            return WrapAsResponseMessageResultAsync(() => dataDelegate(arg1, arg2), logger);
        }
        public static ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T1, T2, T3>(Func<T1, T2, T3, ValueTask> dataDelegate,
            T1 arg1, T2 arg2, T3 arg3,
            ILogger logger)
        {
            return WrapAsResponseMessageResultAsync(() => dataDelegate(arg1, arg2, arg3), logger);
        }

        public static async ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T>(Func<ValueTask<T>> dataDelegate,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            var message = await ResponseExtensions.WrapAsResponseMessageAsync(dataDelegate, logger);
            var result = (wrap ?? WrapResponseMessage)(message);
            return result;
        }
        public static ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T, T1>(
            Func<T1, ValueTask<T>> dataDelegate,
            T1 arg1,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            return WrapAsResponseMessageResultAsync(() => dataDelegate(arg1), logger, wrap);
        }
        public static ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T, T1, T2>(
            Func<T1, T2, ValueTask<T>> dataDelegate,
            T1 arg1, T2 arg2,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            return WrapAsResponseMessageResultAsync(() => dataDelegate(arg1, arg2), logger, wrap);
        }
        public static ValueTask<IActionResult> WrapAsResponseMessageResultAsync<T, T1, T2, T3>(
            Func<T1, T2, T3, ValueTask<T>> dataDelegate,
            T1 arg1, T2 arg2, T3 arg3,
            ILogger logger,
            Func<ResponseMessage<T>, IActionResult> wrap = null)
        {
            return WrapAsResponseMessageResultAsync(() => dataDelegate(arg1, arg2, arg3), logger, wrap);
        }
        #endregion
    }
}