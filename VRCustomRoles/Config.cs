using System.ComponentModel;
using Exiled.API.Interfaces;

namespace VRCustomRoles
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Whether or not debug messages will be shown")]
        public bool Debug { get; set; } = false;
    }
}