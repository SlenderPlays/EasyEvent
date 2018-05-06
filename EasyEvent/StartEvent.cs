using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    class StartEvent : ICommandHandler
    {
        private StartEvent_Plugin plugin;
        public StartEvent(StartEvent_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Start an event based of what is located in config";
        public string GetUsage() => "Used for custom events to diversify the game";
        public void OnCall(ICommandManager manger, string[] args)
        {
            /*if(args.Length == 0)
            {
                plugin.Warn("No paramaters!");
                return;
            }*/
            List<game_Event> events = new List<game_Event>();
            string inconfig = plugin.GetConfigString("easy_events");
            string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
            string normalizedInput=inconfig;
            foreach (string s in charactersToReplace)
            {
                normalizedInput = normalizedInput.Replace(s, "");
            }
            normalizedInput = normalizedInput.Replace(System.Environment.NewLine, "");
            foreach (string Event in normalizedInput.Split('|'))
            {
                List<game_Class> gameClasses = new List<game_Class>();
                foreach (string g_class in Event.Split('(')[1].Split(')')[0].Split('/'))
                {
                    gameClasses.Add(new game_Class(Int32.Parse(g_class.Split(',')[0]), Int32.Parse(g_class.Split(',')[1])));
                }
                events.Add(new game_Event(Event.Split('(')[0], gameClasses));
            }
            //string[] game_Classes = new List<string>();
            //string[] game_precentage = new List<string>();
            if (args.Length == 0)
            {
                string output= null;
                foreach(game_Event Ev in events)
                {
                    output += Ev.name + "\n";
                }
                plugin.Info("Current events:\n" + output);
                plugin.Warn("If you wanted to  start an event, you need to specify the name!");
                return;
            }
            else
            {
                game_Event theEvent = null;
                bool found_Event = false;
                foreach(game_Event Event in events)
                {
                    if(Event.name == args[0])
                    {
                        theEvent = Event;
                        found_Event = true;
                    }
                }
                if(found_Event == false)
                {
                    plugin.Error("No event with the name " + args[0] + "was found!\n Try using one of these:\n");
                    foreach (game_Event Event in events)
                    {
                        plugin.Error(Event.name);
                    }
                }
                else
                {
                    if (theEvent != null)
                    {
                        Server server = PluginManager.Manager.Server;
                        List<Player> players = server.GetPlayers();
                        int playersAssigned = 0;
                       //List<Player> AssignedPlayers = new List<Player>();
                        foreach (game_Class theClass in theEvent.classes)
                        {
                            float precentage = (float)theClass.precentage / 100f;
                            int toAssign = (int)Math.Round(precentage * players.Count());
                            plugin.Info("To assign to: "+ theClass.value+ " to number of people:"+toAssign);
                            playersAssigned += toAssign;
                            for(int i = 0; i<toAssign; i++)
                            {
                                Random rnd = new Random();
                                Player toAssignPlayer = players[rnd.Next(0, players.Count)];
                                players.Remove(toAssignPlayer);
                                toAssignPlayer.ChangeClass((Classes)theClass.value);
                            }
                        }
                        if(players.Any())
                        {
                            Random rnd = new Random();
                            foreach(Player player in players)
                            {

                                
                                int toAssignClass = theEvent.classes[rnd.Next(0, theEvent.classes.Count)].value;
                                player.ChangeClass((Classes)toAssignClass);
                                plugin.Info("Assinging: " + toAssignClass + " to:" + player.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
