using static MDF.DTOS.ApiResult;

namespace MDF.DTOS
{
    public class ApiResult
    {

        public enum ETypeOfApiResultStatusCode
        {
            Default = 0,
            Success = 1,
            NotFound = 2,
            UnAuthentication = 3,
            UnAuthorization = 4,
            BadRequest = 5,
            InternalServerError = 6,
            AlreadyExistData = 7,
        }


        public bool IsSuccess { get; set; }
        public ETypeOfApiResultStatusCode StatusCode { get; set; } = ETypeOfApiResultStatusCode.Default;
        public IEnumerable<string> Messages { get; set; } = new List<string>();

        public ApiResult()
        {
        }

        public override string ToString()
        {
            return
                $"{IsSuccess} - " +
                $"{StatusCode} - " +
                $"[{string.Join(" | ", Messages)}]";
        }


        public virtual ApiResult Success()
        {
            this.IsSuccess = true;
            this.Messages = new List<string>() { "SUCCESS" };
            this.StatusCode = ETypeOfApiResultStatusCode.Success;
            return this;
        }
        public virtual ApiResult BadRequest(params string[] messages) => this.BadRequest(messages.ToList());
        public virtual ApiResult BadRequest(IEnumerable<string> messages)
        {
            this.IsSuccess = false;
            this.Messages = messages;
            this.StatusCode = ETypeOfApiResultStatusCode.BadRequest;
            return this;
        }
        public virtual ApiResult InternalServerError()
        {
            this.StatusCode = ETypeOfApiResultStatusCode.InternalServerError;
            this.IsSuccess = false;
            this.Messages = new List<string>() { "خطایی پیشبینی نشده رخ داده است." };
            return this;
        }

    }
    public class ApiResult<TData> : ApiResult
    {
        public TData Result { get; set; } = default;

        public ApiResult() { }

        public override string ToString()
        {
            return base.ToString() + $" RESULT : {this.Result}";
        }


        public ApiResult<TData> Success(TData data)
        {
            this.IsSuccess = true;
            this.Messages = new List<string>() { "SUCCESS" };
            this.StatusCode = ETypeOfApiResultStatusCode.Success;
            this.Result = data;
            return this;
        }
        public override ApiResult<TData> BadRequest(params string[] messages) => this.BadRequest(messages.ToList());
        public override ApiResult<TData> BadRequest(IEnumerable<string> messages)
        {
            this.IsSuccess = false;
            this.Messages = messages;
            this.StatusCode = ETypeOfApiResultStatusCode.BadRequest;
            this.Result = default;
            return this;
        }
        public override ApiResult<TData> InternalServerError()
        {
            this.StatusCode = ETypeOfApiResultStatusCode.InternalServerError;
            this.IsSuccess = false;
            this.Messages = new List<string>() { "خطایی پیشبینی نشده رخ داده است." };
            this.Result = default;
            return this;
        }

    }

    public static class ApiResultCreator
    {
        public static ApiResult Success()
            => new ApiResult
            {
                IsSuccess = true,
                StatusCode = ETypeOfApiResultStatusCode.Success,
                Messages = new List<string>() { "SUCCESS" },
            };
        public static ApiResult<TData> Success<TData>(TData data)
            => new ApiResult<TData>
            {
                IsSuccess = true,
                Messages = new List<string>(),
                Result = data,
                StatusCode = ETypeOfApiResultStatusCode.Success
            };

        public static ApiResult BadRequest(string message)
            => BadRequest(new List<string>() { message });
        public static ApiResult<TData> BadRequest<TData>(string message)
            => BadRequest<TData>(new List<string>() { message });
        public static ApiResult BadRequest(IEnumerable<string> messages)
            => new ApiResult
            {
                IsSuccess = false,
                StatusCode = ETypeOfApiResultStatusCode.BadRequest,
                Messages = messages,
            };
        public static ApiResult<TData> BadRequest<TData>(IEnumerable<string> messages)
            => new ApiResult<TData>
            {
                IsSuccess = false,
                StatusCode = ETypeOfApiResultStatusCode.BadRequest,
                Messages = messages,
                Result = default
            };
        public static ApiResult<TData> BadRequest<TData>(TData result, string message)
            => new ApiResult<TData>
            {
                IsSuccess = false,
                StatusCode = ETypeOfApiResultStatusCode.BadRequest,
                Messages = new List<string>() { message },
                Result = result
            };


        public static ApiResult UnAuthorization()
            => new ApiResult
            {
                IsSuccess = false,
                StatusCode = ETypeOfApiResultStatusCode.UnAuthorization,
                Messages = new List<string>() { "UNAuthorization" },
            };
        public static ApiResult UnAuthentication(params string[] messages)
            => new ApiResult
            {
                IsSuccess = false,
                StatusCode = ETypeOfApiResultStatusCode.UnAuthentication,
                Messages = messages.ToList(),
            };

        public static ApiResult InternalServerError()
            => new ApiResult
            {
                IsSuccess = false,
                StatusCode = ETypeOfApiResultStatusCode.InternalServerError,
                Messages = new List<string> { "خطایی پیشبینی نشده رخ داده است" }
            };
        public static ApiResult<TData> InternalServerError<TData>()
           => new ApiResult<TData>
           {
               IsSuccess = false,
               StatusCode = ETypeOfApiResultStatusCode.InternalServerError,
               Messages = new List<string> { "خطایی پیشبینی نشده رخ داده است" }
           };
        public static ApiResult<TData> InternalServerError<TData>(string message)
           => new ApiResult<TData>
           {
               IsSuccess = false,
               StatusCode = ETypeOfApiResultStatusCode.InternalServerError,
               Messages = new List<string> { message }
           };


        public static ApiResult NotFound(params string[] messages)
         => new ApiResult
         {
             IsSuccess = false,
             StatusCode = ETypeOfApiResultStatusCode.NotFound,
             Messages = messages.ToList()
         };
        public static ApiResult<TData> NotFound<TData>(params string[] messages)
         => new ApiResult<TData>
         {
             IsSuccess = false,
             StatusCode = ETypeOfApiResultStatusCode.NotFound,
             Messages = messages.ToList(),
             Result = default
         };


        public static ApiResult AlreadyExist(params string[] messages)
         => new ApiResult
         {
             IsSuccess = false,
             StatusCode = ETypeOfApiResultStatusCode.AlreadyExistData,
             Messages = messages.ToList()
         };
        public static ApiResult<TData> AlreadyExist<TData>(params string[] messages)
         => new ApiResult<TData>
         {
             IsSuccess = false,
             StatusCode = ETypeOfApiResultStatusCode.AlreadyExistData,
             Messages = messages.ToList(),
             Result = default
         };


    }

}
