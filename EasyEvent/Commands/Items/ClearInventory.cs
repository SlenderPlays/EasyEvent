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

    public class ClearInventory : ICommandHandler
    {
        private EasyEvents_Plugin plugin;
        public ClearInventory(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Remove an Item from a person";
        public string GetUsage() => "Remove an Item from a person";
        public void OnCall(ICommandManager manger, string[] args)
        {
            Server server = PluginManager.Manager.Server;
            foreach(Player ply in server.GetPlayers())
            {
                foreach(Item item in ply.GetInventory())
                {
                    item.Remove();
                }
            }
        }
    }
}