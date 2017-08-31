using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace TransformerCli
{
    public class Options
    {

        [Option('b', "base", Required = true, HelpText = "Base config file path")]
        public string BaseConfig { get; set; }

        [Option('t', "transform", Required = true, HelpText = "Transform file path")]
        public string TransformConfig { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
