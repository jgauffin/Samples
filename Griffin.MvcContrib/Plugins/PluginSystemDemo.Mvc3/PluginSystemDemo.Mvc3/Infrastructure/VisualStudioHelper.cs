using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PluginSystemDemo.Mvc3.Infrastructure
{
    public static class VisualStudioHelper
    {
        public static bool IsInVisualStudio
        {
            get
            {
                if (Debugger.IsAttached)
                    return true;

                using (var process = Process.GetCurrentProcess())
                {
                    return process.ProcessName.ToLowerInvariant().Contains("devenv");
                }
            }
        }
    }
}