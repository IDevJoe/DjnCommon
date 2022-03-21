using System;
using System.Diagnostics;
using Serilog;
using Serilog.Core;

namespace DjnCommon
{
    public static class DjnLogging
    {
        private static bool _isInit = false;
        private static Logger _logger;
        
        private static void _init()
        {
            if (_isInit) return;
            _isInit = true;
            LoggerConfiguration conf = new LoggerConfiguration().WriteTo.Console();
            if (Debugger.IsAttached)
            {
                Console.WriteLine("DEBUGGER ATTACHED. LOGGING VERBOSE.");
                conf = conf.MinimumLevel.Verbose();
            }
            else
            {
                conf = conf.MinimumLevel.Information();
            }

            _logger = conf.CreateLogger();
        }

        public static Logger GetLogger()
        {
            _init();
            return _logger;
        }
    }
}