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

    public class GiveItem : ICommandHandler
    {



        private EasyEvents_Plugin plugin;
        public GiveItem(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Give Item to a person";
        public string GetUsage() => "Give Item to a person";
        public void OnCall(ICommandManager manger, string[] args)
        {
            Server server = PluginManager.Manager.Server;
            if (args.Length < 2)
            {
                plugin.Error("Insufficient data! Example:   give_item Joe E11    or  give_item Bob 20");
                return;
            }
            int weponId = -1;
            bool usingItemID = Int32.TryParse(args[1], out weponId);

            Player thePlayer = null;

            if (usingItemID)
            {

            }
            else
            {
                weponId = -1;
                #region Get The Bloddy Item

                List<String> items = new List<string>();
                items.AddRange(Extensions.ListItemTypes().ToStringList());

                //Debug("Before Enum Parse: "+args[1].ToUpper().InclusiveListLevDistance(items));

                weponId = ((ItemType)Enum.Parse(typeof(ItemType), args[1].ToUpper().InclusiveListLevDistance(items))).GetValue();
                Debug("Got the item ID: " + weponId.ToString());

                #endregion
            }
            #region Get the Player

            List<String> playerNames = new List<String>();

            foreach (Player ply in server.GetPlayers())
            {
                playerNames.Add(ply.Name);
                Debug("Adding: " + ply.Name);
            }
            //Debug("Should be:" + args[0].ListLevDistance(playerNames));
            string playerName = args[0].InclusiveListLevDistance(playerNames);
            Debug("Got the player: " + playerName);
            foreach (Player ply in server.GetPlayers())
            {
                if (ply.Name == playerName)
                {
                    thePlayer = ply;
                }
            }

            #endregion
            if (thePlayer != null)
            {
                thePlayer.GiveItem((ItemType)weponId);
            }
        }

        private void Debug(string input)
        {
            plugin.Info(input);
        }
    }
}