using Model.Other;

namespace Fresh.Config
{
    public class ResultHelper
    {
        public static async Task<ApiResult> Success(object data)
        {
            var res = Task.Run(() =>
            {
                return new ApiResult() { IsSuccess = true, Result = data };
            });
            return await res;
        }
        public static async Task<ApiResult> Error(string message)
        {
            var res = Task.Run(() =>
            {
                return new ApiResult() { IsSuccess = false, Msg = message };
            });
            return await res;
        }
    }
}
