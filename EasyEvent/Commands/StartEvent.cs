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

using ZeroExtensions;

namespace EasyEvent
{

    public class StartEvent : ICommandHandler
    {

        public List<game_Event> events = new List<game_Event>();


        private EasyEvents_Plugin plugin;
        public StartEvent(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Start an event based of what is located in config";
        public string GetUsage() => "Used for custom events to diversify the game";
        public void OnCall(ICommandManager manger, string[] args)
        {
            InterpreteConfig();
            if (args.Length == 0)
            {
                PrintConfig();
                return;
            }

            bool foundEvent = FindEvent(args[0]);

            if (!foundEvent)
            {
                plugin.Error("No event found with that name!");
                PrintConfig();
                return;
            }
            game_Event theEvent = null;
            try
            {
                theEvent = GetEvent(args[0]);
            }
            catch (Exception e)
            {
                plugin.Error(e.Message);
            }
            Server server = PluginManager.Manager.Server;
            Random rnd = new Random();
            List<game_Player> players = new List<game_Player>();
            foreach(Player ply in server.GetPlayers())
            {
                players.Add(new game_Player(ply));
            }
            players.NShuffle(rnd, 4);
            Debug("Shufffled");
            foreach (game_Class gameClass in theEvent.classes)
            {
                float percentage = (float)(gameClass.precentage / 100f);
                int numberofplayers = (int)(Math.Floor(percentage * players.Count));
                Classes classToChange = (Classes)gameClass.value;
                Debug(percentage.ToString());
                Debug(numberofplayers.ToString());
                for (int i = 0; i < numberofplayers; i++)
                {
                    int index = rnd.Next(0, players.Count);
                    if (players[index].assigned_to == true)
                    {
                        --i;
                    }
                    else
                    {
                        Debug("Setting player " + players[index].Player.Name+ " to: " + classToChange.ToString());
                        players[index].assigned_to = true;
                        players[index].Player.Kill();
                        players[index].Player.ChangeClass(classToChange,true,true);
                    }
                }
            }
            if (AllPlayersAssigned(players))
            {

            }
            else
            {
                Debug("Setting skipped players's classes.");
                foreach (game_Player ply in players)
                {
                    if (ply.assigned_to == true)
                    {

                    }
                    else
                    {
                        int calculated_percent = 0;
                        int chosenClass_percent = rnd.Next(0, 101);

                        foreach (game_Class gameClass in theEvent.classes)
                        {
                            if (chosenClass_percent >= calculated_percent && chosenClass_percent < calculated_percent + gameClass.precentage)
                            {
                                Classes classToChange = (Classes)gameClass.value;
                                Debug("Setting player " + ply.Player.Name + " to: " + classToChange.ToString());
                                ply.Player.Kill();
                                ply.Player.ChangeClass(classToChange, true, true);
                                calculated_percent = 105;
                            }
                            calculated_percent += gameClass.precentage;
                        }
                    }
                }
            }

            }
        

        private game_Event GetEvent(string name)
        {
            foreach (game_Event ev in events)
            {
                if (ev.name == name)
                {
                    return ev;
                }
            }
            throw new Exception("No event found with that name!");
        }

        private bool FindEvent(string name)
        {
            foreach (game_Event ev in events)
            {
                if (ev.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void PrintConfig()
        {
            
            string output = "\n";
            foreach (game_Event ev in events)
            {
                output += ev.name;
                foreach (game_Class inClass in ev.classes)
                {
                    Classes tempClass = (Classes)inClass.value;
                    output += "    " + tempClass.ToString() + "(" + inClass.value + ")" + "-" + inClass.precentage + "%";

                }
                output += "\n";
            }
            Debug(output);
        }

        public void InterpreteConfig()
        {
            events.Clear();
            /* Example of Config File
             * easy_events=
             * D_And_S{50%1|50%6}
             * C_vs_NTF{40%8|55%11|5%,4};
             */

            // Read the config file
            string inconfig = plugin.GetConfigString("easy_events");

            //Remove those annoying spaces
            string normalizedInput = inconfig.Replace(" ", "");

            //Split by NewLine
            string[] lines = normalizedInput.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
            );

            foreach (string UnFilteredEvent in lines)
            {
                // Get Event Name
                string EventName = UnFilteredEvent.Split('{')[0];

                //Create a list of in-game Classes
                List<game_Class> gameClasses = new List<game_Class>();

                // Get the whole paramater (The thing between the { and })
                string Parameter = UnFilteredEvent.Split('{')[1].Split('}')[0];

                //A list of all the arguments
                string[] Ev_Args = Parameter.Split('|');

                int prTotal = 0;
                foreach (string argument in Ev_Args)
                {
                    string classValue_string = argument.Split('%')[1];
                    string classPercent_string = argument.Split('%')[0];

                    int classValue;
                    int classPercent;

                    bool parse_Value = Int32.TryParse(classValue_string, out classValue);
                    bool parse_Percent = Int32.TryParse(classPercent_string, out classPercent);
                    if (!parse_Percent || !parse_Value || classPercent > 100)
                    {
                        plugin.Error("Invalid precentage,it must be a whole number between 0 to 100! Event name: " + EventName);
                    }
                    prTotal += classPercent;
                    gameClasses.Add(new game_Class(classValue, classPercent));
                }
                if (prTotal > 100)
                {
                    plugin.Error("All precentages must add up to 100! Event name: " + EventName);
                }
                prTotal = 0;
                events.Add(new game_Event(EventName, gameClasses));
            }
        }
        private bool AllPlayersAssigned(List<game_Player> list)
        {
            bool toReturn = true;
            foreach(game_Player ply in list)
            {
                if(ply.assigned_to == false)
                {
                    toReturn = false;
                }
            }
            return toReturn;
        }

        private void Debug(string input)
        {
            plugin.Info(input);
        }
    }
}
