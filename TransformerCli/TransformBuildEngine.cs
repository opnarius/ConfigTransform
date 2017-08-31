using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace TransformerCli
{
    public class TransformBuildEngine : IBuildEngine
    {
        public void LogErrorEvent(BuildErrorEventArgs e)
        {
            Console.WriteLine(LogFormat, "ERROR", e.Message);
        }

        public void LogWarningEvent(BuildWarningEventArgs e)
        {
            Console.WriteLine(LogFormat, "WARNING", e.Message);
        }

        public void LogMessageEvent(BuildMessageEventArgs e)
        {
            Console.WriteLine(LogFormat, e.Importance, e.Message);
        }

        public void LogCustomEvent(CustomBuildEventArgs e)
        {
            Console.WriteLine(LogFormat, "CUSTOM", e.Message);
        }

        public bool BuildProjectFile(
            string projectFileName, string[] targetNames,
            IDictionary globalProperties, IDictionary targetOutputs)
        {
            return true;
        }

        public bool ContinueOnError
        {
            get { return true; }
        }

        public int LineNumberOfTaskNode
        {
            get { return 0; }
        }

        public int ColumnNumberOfTaskNode
        {
            get { return 0; }
        }

        public string ProjectFileOfTaskNode
        {
            get { return string.Empty; }
        }

        private const string LogFormat = "{0} : {1}";
    }
}
