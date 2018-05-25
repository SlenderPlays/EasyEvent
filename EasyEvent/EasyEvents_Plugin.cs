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

using ZeroExtensions;

using EasyEvent.Handlers;

namespace EasyEvent
{
    [PluginDetails(
        author = "Zero",
        name = "EasyEvent",
        description = "Start an event based of what is located in config",
        id = "Zero.EasyEvent",
        version = "2.0",
        SmodMajor = 2,
        SmodMinor = 1,
        SmodRevision = 0
        )]
    public class EasyEvents_Plugin : Plugin
    {
        public override void OnDisable()
        {
            this.Warn("Easy Event has been disabled!");
        }

        public override void OnEnable()
        {
            this.Info("Easy Event Plugin has loaded :)");
            //this.Info("Config value: " + this.GetConfigString("test"));


            
            /*foreach(string idk in Extensions.ListItemTypes().ToStringList())
            {
                this.Info(idk);
            }
            Info(Environment.NewLine);
            foreach (string idk in Extensions.ListClasses().ToStringList())
            {
                this.Info(idk);
            }*/
        }

        public override void Register()
        {
            StartEvent startEvent = new StartEvent(this);
            this.Info("Checking the config...");
            // Register Events
            this.AddEventHandler(typeof(IEventRoundStart), new RoundStartHandler(this, startEvent), Priority.High);
            // Register Commands
            this.AddCommand("start_event", startEvent);
            this.AddCommand("give_item", new GiveItem(this));
            this.AddCommand("give_item_steamid", new GiveItemSteamID(this));
            this.AddCommand("list_players", new ListPlayers(this));
            this.AddCommand("give_class", new GiveClass(this));
            this.AddCommand("remove_item", new RemoveItem(this));
            this.AddCommand("remove_item_steamid", new RemoveItemSteamID(this));
            this.AddCommand("remove_class", new RemoveClass(this));
            this.AddCommand("clear_inventory", new ClearInventory(this));
            // Register config settings
            this.AddConfig(new ConfigSetting("easy_events", "", Smod2.Config.SettingType.STRING, true, "get events of server"));
            this.AddConfig(new ConfigSetting("auto_events", false, Smod2.Config.SettingType.BOOL, true, "enable or disable auto-events"));
            this.AddConfig(new ConfigSetting("auto_events_interval", -1, Smod2.Config.SettingType.NUMERIC, true, "enable or disable auto-events"));


            /*startEvent.InterpreteConfig();
            string events = Environment.NewLine ;
            foreach (game_Event ev in startEvent.events)
            {
                events += ev.name + Environment.NewLine;
            }
            this.Info("Configs:" + Environment.NewLine + 
                "Events: " + events +
                "Auto-events: " + this.GetConfigBool("auto_events") + Environment.NewLine +
                "Auto-events interval: " + this.GetConfigInt("auto_events_interval")
                );
            
            */
        }
    }
}