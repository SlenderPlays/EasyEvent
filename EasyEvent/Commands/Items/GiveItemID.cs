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

    public class GiveItemID : ICommandHandler
    {



        private EasyEvents_Plugin plugin;
        public GiveItemID(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
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


            foreach (Player ply in server.GetPlayers())
            {
                if(ply.SteamId == args[0])
                {
                    thePlayer = ply;
                }
            }
            //Debug("Should be:" + args[0].ListLevDistance(playerNames));
            Debug("Got the player: " + thePlayer.Name + " with steamID: " +thePlayer.SteamId);
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