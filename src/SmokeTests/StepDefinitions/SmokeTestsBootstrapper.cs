﻿using System.Diagnostics;
using System.Linq;
using TechTalk.SpecFlow;

namespace ClearMeasure.Bootcamp.SmokeTests.StepDefinitions
{
    [Binding]
    public static class SmokeTestsBootstrapper
    {
        private static Process _iisProcess;

        [BeforeTestRun]
        public static void Startup()
        {
            // kill off existing IIS Express instance if present
            var matchingProcess = Process.GetProcessesByName("iisexpress").FirstOrDefault();
            matchingProcess?.Kill();
            _iisProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = SmokeTestPaths.GetIisExpressExecPath(),
                        Arguments = SmokeTestPaths.GetIisExpressExecArguments()
                    }
                };
            _iisProcess.Start();
        }
        
        [AfterTestRun]
        public static void Cleanup()
        {
            // stop IIS Express
            _iisProcess?.Kill();
        }
    }
}