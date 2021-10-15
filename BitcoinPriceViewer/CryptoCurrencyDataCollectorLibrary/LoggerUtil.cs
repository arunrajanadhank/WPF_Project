using log4net;
using System;
using System.Linq;

namespace CryptoCurrencyDataCollector
{
    public static class LoggerUtil
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region private methods

        private static string _GetMessagePrefix(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            return $"[{GetBaseName(sourceFilePath)}::{memberName}::{sourceLineNumber}] ";
        }

        private static void _LogInfo(string message, char paddingChar, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            try
            {
                string newMessage = _GetMessagePrefix(memberName, sourceFilePath, sourceLineNumber) + message;

                logger.Info(newMessage);
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }

        private static void _LogError(string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            try
            {
                string newMessage = _GetMessagePrefix(memberName, sourceFilePath, sourceLineNumber) + message;

                logger.Info(newMessage);

                logger.Error(newMessage);
            }
            catch (Exception)
            {
                // Do nothing
            }
        }

        private static void _LogError(Exception ex, string customMessage, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            string message = $"[Message={ex.Message}, HResult= 0x{ex.HResult.ToString("X")}][{customMessage}]";

            _LogError(message, memberName, sourceFilePath, sourceLineNumber);
        }

        private static string GetBaseName(string file)
        {
            string className = file;

            if (string.IsNullOrEmpty(className))
                return string.Empty;

            try
            {
                className = file.Split('\\').Last().Split('.').First();
            }
            catch (Exception)
            {
                // Do nothing
            }

            return className;
        }
        #endregion

        #region public methods

        public static void LogException(Exception ex, string customMessage = "",
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (ex is AggregateException)
            {
                AggregateException ae = (AggregateException)ex;

                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    _LogError(e, customMessage, memberName, sourceFilePath, sourceLineNumber);
                }
            }
            else
            {
                _LogError(ex, customMessage, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        internal static void LogError(string message,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            _LogError(message, memberName, sourceFilePath, sourceLineNumber);
        }

        public static void LogInfo(string message,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            _LogInfo(message, ' ', memberName, sourceFilePath, sourceLineNumber);
        }
        #endregion

    }
}
