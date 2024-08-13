using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BasicCustomHUD
{
    public class Config : IConfig
    {
        [Description("Indicates whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enables or disables debug mode.")]
        public bool Debug { get; set; } = false;

        [Description("Interval in seconds at which the game time will be updated on the screen.")]
        public float UpdateInterval { get; set; } = 1f;
    }
}