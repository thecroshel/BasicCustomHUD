using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BasicCustomHUD
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Enables or disables debug mode.")]
        public bool Debug { get; set; } = false;

        [Description("Interval in seconds to update the hint.")]
        public float UpdateInterval { get; set; } = 1f;

        [Description("The first line of text to display.")]
        public string FirstLineText { get; set; } = "FIRSTLINE";

        [Description("The second line of text to display.")]
        public string SecondLineText { get; set; } = "SECONDLINE";

        [Description("The third line of text to display.")]
        public string ThirdLineText { get; set; } = "THIRDLINE";
    }
}