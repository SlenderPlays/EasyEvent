using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Commands;
using Smod2.Config;
using Smod2.Events;
using Smod2.Logging;

namespace EasyEvent
{
    [PluginDetails(
        author = "Zero",
        name = "EasyEvent",
        description = "Start an event based of what is located in config",
        id = "Zero.EasyEvent",
        version = "1.0",
        SmodMajor = 2,
        SmodMinor = 0,
        SmodRevision = 0
        )]
    public class StartEvent_Plugin : Plugin
    {
        public override void OnDisable()
        {
            this.Warn("Easy Event has been disabled!");
        }

        public override void OnEnable()
        {
            this.Info("Easy Event Plugin has loaded :)");
            //this.Info("Config value: " + this.GetConfigString("test"));
        }

        public override void Register()
        {
            // Register Events
            //this.AddEventHandler(typeof(IEventRoundStart), new RoundStartHandler(this), Priority.Highest);
            // Register Commands
            this.AddCommand("start_event", new StartEvent(this));
            // Register config settings
            this.AddConfig(new ConfigSetting("easy_events", "", Smod2.Config.SettingType.STRING, true, "get events of server"));
        }
    }
}