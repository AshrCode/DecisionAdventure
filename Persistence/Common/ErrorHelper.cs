using Common;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ErrorHelper
    {
        public static Task LogAndRaiseError(string errMessage, int apiErrorCode, ILogger logger, string stackTrace = null)
        {
            logger.LogError(errMessage, stackTrace);

            throw new ApiException(apiErrorCode, errMessage);
        }
    }
}
