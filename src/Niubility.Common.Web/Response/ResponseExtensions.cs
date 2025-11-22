using Microsoft.Extensions.Logging;
using Niubility.Core;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Niubility.Common.Web
{
    public static class ResponseExtensions
    {
        public static async ValueTask<ResponseMessage<T>> WrapResponseMessageAsync<T>(Func<ValueTask<ResponseMessage<T>>> dataDelegate,
            ILogger logger)
        {
            try
            {
                return await dataDelegate.Invoke();
            }
            catch (NiubilityException iaException)
            {
                return WrapAsResponseMessage<T>(iaException);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error on wrap response message");
                return WrapAsResponseMessage<T>(
                    new NiubilityApplicationException(exception));
            }
        }


        public static ResponseMessage<T> WrapAsResponseMessage<T>(T data)
        {
            return new ResponseMessage<T>
            {
                Success = true,
                Data = data,
                HttpStatusCode = (int)HttpStatusCode.OK
            };
        }

        public static ResponseMessage<T> WrapAsResponseMessage<T>(
            in NiubilityException exception,
            T data = default)
        {
            return new ResponseMessage<T>
            {
                Success = false,
                Data = data,
                ErrorCode = exception.ErrorCode,
                HttpStatusCode = exception.HttpStatusCode,
                Message = exception.Message
            };
        }

        public static ResponseMessage<T> WrapAsResponseMessage<T>(Func<T> dataDelegate,
            ILogger logger)
        {
            try
            {
                var data = dataDelegate.Invoke();
                return WrapAsResponseMessage(data);
            }
            catch (NiubilityException iaException)
            {
                return WrapAsResponseMessage<T>(iaException);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error on wrap response message");
                return WrapAsResponseMessage<T>(
                    new NiubilityApplicationException(exception));
            }
        }

        public static async ValueTask<ResponseMessage<bool>> WrapAsResponseMessageAsync(Func<ValueTask> dataDelegate,
            ILogger logger)
        {
            try
            {
                await dataDelegate.Invoke();
                return WrapAsResponseMessage(true);
            }
            catch (NiubilityException iaException)
            {
                return WrapAsResponseMessage<bool>(iaException);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error on wrap response message");
                return WrapAsResponseMessage<bool>(
                    new NiubilityApplicationException(exception));
            }
        }
        public static async ValueTask<ResponseMessage<T>> WrapAsResponseMessageAsync<T>(Func<ValueTask<T>> dataDelegate,
            ILogger logger)
        {
            try
            {
                var data = await dataDelegate.Invoke();
                return WrapAsResponseMessage(data);
            }
            catch (NiubilityException iaException)
            {
                return WrapAsResponseMessage<T>(iaException);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error on wrap response message");
                return WrapAsResponseMessage<T>(
                    new NiubilityApplicationException(exception));
            }
        }
    }
}