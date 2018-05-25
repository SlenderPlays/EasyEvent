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

    public class RemoveItemSteamID : ICommandHandler
    {



        private EasyEvents_Plugin plugin;
        public RemoveItemSteamID(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Give Item to a person";
        public string GetUsage() => "Give Item to a person";
        public void OnCall(ICommandManager manger, string[] args)
        {
            Server server = PluginManager.Manager.Server;
            if (args.Length < 2)
            {
                plugin.Error("Insufficient data! Example:   give_item_id 93423842934... E11    or  give_item_id 4534534543... 20");
                return;
            }
            Player thePlayer = null;

            if (args[1].ToUpper() == "ALL")
            {
                #region Get the Player2

                foreach (Player ply in server.GetPlayers())
                {
                    if (ply.SteamId == args[0])
                    {
                        thePlayer = ply;
                    }
                }
                //Debug("Should be:" + args[0].ListLevDistance(playerNames));
                Debug("Got the player: " + thePlayer.Name + " with steamID: " + thePlayer.SteamId);

                #endregion
                foreach (Item item in thePlayer.GetInventory())
                {
                    item.Remove();
                }
                return;
            }

            int weponId = -1;
            bool usingItemID = Int32.TryParse(args[1], out weponId);

            

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


            foreach (Player ply in server.GetPlayers())
            {
                if (ply.SteamId == args[0])
                {
                    thePlayer = ply;
                }
            }
            //Debug("Should be:" + args[0].ListLevDistance(playerNames));
            Debug("Got the player: " + thePlayer.Name + " with steamID: " + thePlayer.SteamId);
            #endregion
            if (thePlayer != null)
            {
                foreach (Item item in thePlayer.GetInventory())
                {
                    if (item.ItemType == (ItemType)weponId)
                    {
                        item.Remove();
                    }
                }
            }
        }

        private void Debug(string input)
        {
            plugin.Info(input);
        }
    }
}