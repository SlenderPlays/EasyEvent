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

    public class ListPlayers : ICommandHandler
    {



        private EasyEvents_Plugin plugin;
        public ListPlayers(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "List the online players.";
        public string GetUsage() => "List the online players.";
        public void OnCall(ICommandManager manger, string[] args)
        {
            Server server = PluginManager.Manager.Server;
            string output;
            output = Environment.NewLine;
            foreach (Player ply in server.GetPlayers())
            {
                output += ply.Name + "  " + ply.SteamId + "  " + ply.IpAddress + "  "+ ply.Class.ToString() + Environment.NewLine;
            }
            Debug(output);
        }
        private void Debug(string input)
        {
            plugin.Info(input);
        }
    }
}