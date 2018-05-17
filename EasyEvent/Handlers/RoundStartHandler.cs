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



namespace EasyEvent.Handlers
{
    public class RoundStartHandler : IEventRoundStart 
    {
        private Plugin plugin;
        private StartEvent startEvent;
        Random rnd = new Random();

        public RoundStartHandler(Plugin plugin, StartEvent startEvent)
        {
            this.plugin = plugin;
            this.startEvent = startEvent;
        }
        public int rounds_passed { get; set; } = 0;
        public void OnRoundStart(Server server)
        {
            startEvent.InterpreteConfig();
            plugin.Info("Generating a random number...");
            /*plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());
            plugin.Info(rnd.Next(0, startEvent.events.Count).ToString());*/

            rounds_passed++;
            plugin.Info("Number of rounds passed ... " + rounds_passed);
            

            bool automatic = plugin.GetConfigBool("auto_events");
            if(automatic)
            {
                if(rounds_passed == plugin.GetConfigInt("auto_events_interval")+1)
                {
                    rounds_passed = 0;
                    
                    int randomNR = rnd.Next(0, startEvent.events.Count-1);
                    plugin.Info("Choosing "+randomNR+" event out of " + startEvent.events.Count);
                    startEvent.OnCall(plugin.pluginManager.CommandManager, new string[]{ startEvent.events[randomNR].name});
                }
            }
        }
    }
}
