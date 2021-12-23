using System;

using System.Diagnostics;

using System.Text;

using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Logger
{
    /// <summary>
    /// This class to initialize the SDK logger.
    /// </summary>
    public class SDKLogger
    {
        static SDKLogger logger;

        static TraceSwitch logSwitch;

        static TextWriterTraceListener defaultTrace;

        public SDKLogger()
        {
        }

        /// <summary>
        /// Creates an User SDKLogger instance with the specified Logger class instance. 
        /// </summary>
        /// <param name="logger">A Logger class instance.</param>
        internal static void Initialize(Logger logger)
        {
            Trace.AutoFlush = true;

            SDKLogger.logger = new SDKLogger();

            logSwitch = new TraceSwitch(Constants.LOGGER, Constants.LOGGER_NAME);

            logSwitch.Level = (TraceLevel)logger.Level;

            if(TraceLevel.Off.Equals((TraceLevel)logger.Level))
            {
                return;
            }

            defaultTrace = new TextWriterTraceListener(logger.FilePath);

            Trace.Listeners.Add(defaultTrace);
        }

        public static void LogInfo(string message)
        {
            if (logSwitch.TraceInfo)
            {
                logger.Log(message, Constants.LOG_INFO);
            }
        }

        void Log(string message, string messageHeader)
        {
            var timestamp = GetTimeStamp();

            if (Trace.Listeners.Count > 1 && defaultTrace != null)
            {
                defaultTrace.WriteLine(message, timestamp + " [" + messageHeader + "]");

                defaultTrace.Flush();
            }
            else
            {
                Trace.WriteLine(message, timestamp + " [" + messageHeader + "]");
            }
        }

        public static void LogError(string message)
        {
            if (logSwitch.TraceError)
            {
                logger.Log(message, Constants.LOG_ERROR);
            }
        }

        public static void LogWarning(string message)
        {
            if (logSwitch.TraceWarning)
            {
                logger.Log(message, Constants.LOG_WARNING);
            }
        }

        internal static void LogError(Exception exception)
        {
            var stackTrace = new StackTrace(exception, true);

            var frameCount = stackTrace.FrameCount;

            var errorMessage = new StringBuilder();

            for (var i = 0; i < frameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);

                errorMessage.AppendLine("Exception in Type: " + frame.GetMethod().DeclaringType.Name + " at Method: " + frame.GetMethod() + " in File: " + frame.GetFileName() + " at Line: " + frame.GetFileLineNumber());
            }

            errorMessage.AppendLine(exception.Message);

            if (exception.InnerException != null)
            {
                errorMessage.AppendLine(exception.InnerException.Message);
            }

            LogError(errorMessage.ToString());
        }

        static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
        }
    }
}
