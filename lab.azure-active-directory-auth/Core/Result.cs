using lab.azure_active_directory_auth.Helpers;

namespace lab.azure_active_directory_auth.Core
{
    public class Result
    {
        public bool Success { get; }

        public string? Message { get; }

        public string? MessageType { get; }

        public string? ParentId { get; }

        public string? ParentName { get; }

        public bool HasRecord { get; }

        public object? Data { get; }

        public int ResultCount { get; }

        public string? ResultId { get; }

        public string? RedirectUrl { get; set; }

        public string? UploadPath { get; }

        public Result()
        {
        }

        private Result(bool success, string message, string messageType)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
        }

        private Result(bool success, string message, string messageType, string redirectUrl)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            RedirectUrl = redirectUrl;
        }

        private Result(bool success, string message, string messageType, string parentId, string parentName)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            ParentId = parentId;
            ParentName = parentName;
        }

        private Result(bool success, string message, string messageType, object data)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            Data = data;
        }

        private Result(bool success, string message, string messageType, string redirectUrl, object data)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            RedirectUrl = redirectUrl;
            Data = data;
        }

        private Result(bool success, string message, string messageType, string parentId, string parentName, object data)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            ParentId = parentId;
            ParentName = parentName;
            Data = data;
        }

        public static Result Info()
        {
            return new Result(false, MessageHelper.Info, MessageHelper.MessageTypeInfo);
        }

        public static Result Info(string message)
        {
            return new Result(false, message, MessageHelper.MessageTypeInfo);
        }

        public static Result Warning()
        {
            return new Result(false, MessageHelper.Warning, MessageHelper.MessageTypeWarning);
        }

        public static Result Warning(string message)
        {
            return new Result(false, message, MessageHelper.MessageTypeWarning);
        }

        public static Result Fail()
        {
            return new Result(false, MessageHelper.Error, MessageHelper.MessageTypeDanger);
        }

        public static Result Fail(string message)
        {
            return new Result(false, message, MessageHelper.MessageTypeDanger);
        }

        public static Result Ok()
        {
            return new Result(true, MessageHelper.Success, MessageHelper.MessageTypeSuccess);
        }

        public static Result Ok(string message)
        {
            return new Result(true, message, MessageHelper.MessageTypeSuccess);
        }

        public static Result Ok(string message, string parentId, string parentName)
        {
            return new Result(true, message, MessageHelper.MessageTypeSuccess, parentId, parentName);
        }

        public static Result Ok(string message, string redirectUrl)
        {
            return new Result(true, message, MessageHelper.MessageTypeSuccess, redirectUrl);
        }

        public static Result Ok(string message, object data)
        {
            return new Result(true, message, MessageHelper.MessageTypeSuccess, data);
        }

        public static Result Ok(string message, string redirectUrl, object data)
        {
            return new Result(true, message, MessageHelper.MessageTypeSuccess, redirectUrl, data);
        }

        public static Result Ok(string message, string parentId, string parentName, object data)
        {
            return new Result(true, message, MessageHelper.MessageTypeSuccess, parentId, parentName, data);
        }
    }
}
