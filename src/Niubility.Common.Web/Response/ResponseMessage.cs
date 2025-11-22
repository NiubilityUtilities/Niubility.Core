using Newtonsoft.Json;

namespace Niubility.Common.Web
{
    public class ResponseMessage<T>
    {
        /// <summary>
        /// Is success, if false please refer the error code and message.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public int HttpStatusCode { get; set; }
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Error Code
        /// </summary>
        [JsonProperty("code")]
        public string ErrorCode { get; set; }
        /// <summary>
        /// Info/Error description
        /// </summary>
        public string Message { get; set; }
    }
}