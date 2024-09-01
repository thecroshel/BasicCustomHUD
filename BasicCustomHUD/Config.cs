using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BasicCustomHUD
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
    }
}