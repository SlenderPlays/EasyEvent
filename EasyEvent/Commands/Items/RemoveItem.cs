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

    public class RemoveItem : ICommandHandler
    {



        private EasyEvents_Plugin plugin;
        public RemoveItem(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Remove an Item from a person";
        public string GetUsage() => "Remove an Item from a person";
        public void OnCall(ICommandManager manger, string[] args)
        {
            Server server = PluginManager.Manager.Server;
            if (args.Length < 2)
            {
                plugin.Error("Insufficient data! Example:   give_item Joe E11    or  give_item Bob 20");
                return;
            }

            Player thePlayer = null;

            if (args[1].ToUpper() == "ALL")
            {
                #region Get the Player2

                List<String> playerNames2 = new List<String>();

                foreach (Player ply in server.GetPlayers())
                {
                    playerNames2.Add(ply.Name);
                    Debug("Adding: " + ply.Name);
                }
                //Debug("Should be:" + args[0].ListLevDistance(playerNames));
                string playerName2 = args[0].InclusiveListLevDistance(playerNames2);
                Debug("Got the player: " + playerName2);
                foreach (Player ply in server.GetPlayers())
                {
                    if (ply.Name == playerName2)
                    {
                        thePlayer = ply;
                    }
                }

                #endregion
                foreach(Item item in thePlayer.GetInventory())
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
                items.AddRange(new String[] {
                "NULL",
                "JANITOR_KEYCARD",
                "SCIENTIST_KEYCARD",
                "MAJOR_SCIENTIST_KEYCARD",
                "ZONE_MANAGER_KEYCARD",
                "GUARD_KEYCARD",
                "SENIOR_GUARD_KEYCARD",
                "CONTAINMENT_ENGINEER_KEYCARD",
                "MTF_LIEUTENANT_KEYCARD",
                "MTF_COMMANDER_KEYCARD",
                "FACILITY_MANAGER_KEYCARD",
                "CHAOS_INSURGENCY_DEVICE",
                "O5_LEVEL_KEYCARD",
                "RADIO",
                "M1911_PISTOL",
                "MEDKIT",
                "FLASHLIGHT",
                "MICROHID",
                "COIN",
                "CUP",
                "AMMOMETER",
                "E11_STANDARD_RIFLE",
                "SBX7_PISTOL",
                "DROPPED_SFA",
                "SKORPION_SMG",
                "LOGICER",
                "POSITRON_GRENADE",
                "SMOKE_GRENADE",
                "DISARMER",
                "DROPPED_RAT",
                "DROPPED_PAT"
            });

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
                
                foreach(Item item in thePlayer.GetInventory())
                {
                    if(item.ItemType == (ItemType)weponId)
                    {
                        item.Remove();
                    }
                }
                //thePlayer.GiveItem((ItemType)weponId);
            }
        }

        private void Debug(string input)
        {
            plugin.Info(input);
        }
    }
}