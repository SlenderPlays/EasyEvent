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

    public class GiveTeam : ICommandHandler
    {



        private EasyEvents_Plugin plugin;
        public GiveTeam(EasyEvents_Plugin plugin_in) => this.plugin = plugin_in;
        public string GetCommandDescription() => "Too bored to type.";
        public string GetUsage() => "Same dude.";
        public void OnCall(ICommandManager manger, string[] args)
        {
            Server server = PluginManager.Manager.Server;
            if (args.Length < 2)
            {
                plugin.Error("Insufficient data! Example:   give_group Class-D E11    or  give_group 4 20");
                return;
            }
            int weponId = -1;
            bool usingItemID = Int32.TryParse(args[1], out weponId);

            int classId = -1;

            bool usingClassID = Int32.TryParse(args[0], out classId);
            
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
            #region GiveDboizTheWepon

            Classes classWithItem = Classes.UNASSIGNED;
            //args[0].Split(new char[] {']'}, 3);
            /*
             * server.WritePart(text2.Split(new char[]
					{
						']'
					}, 3)[2], color4, 0, false, true);
                    */
            bool doingEveryone = false;
            if (usingClassID)
            {
                classWithItem = (Classes)classId;
            }
            else
            {
                if(args[0].ToUpper() == "ALL")
                {
                    doingEveryone = true;
                }
                else
                {
                    List<String> classes_list = new List<string>();
                    classes_list.AddRange(new String[] {
                    "UNASSIGNED",
                    "SCP_173",
                    "CLASSD",
                    "SPECTATOR",
                    "SCP_106",
                    "NTF_SCIENTIST",
                    "SCP_049",
                    "SCIENTIST",
                    "SCP_079",
                    "CHAOS_INSUGENCY",
                    "SCP_096",
                    "SCP_049_2",
                    "ZOMBIE",
                    "NTF_LIEUTENANT",
                    "NTF_COMMANDER",
                    "NTF_GUARD",
                    "TUTORIAL"
                    });
                    string closest = args[0].ToUpper().InclusiveListLevDistance(classes_list);
                    Debug("Closest: " + closest);
                    classWithItem = (Classes)(Enum.Parse(typeof(Classes),closest));
                    Debug("Giving class " + classWithItem.ToString() + " item " + weponId);
                }
            }
            #endregion
            foreach(Player ply in server.GetPlayers())
            {
                if(doingEveryone)
                {
                    ply.GiveItem((ItemType)weponId);
                }
                else
                {
                    Debug("Final thingy-mabob: " + ply.Class.ClassType.ToString().ToUpper() + "    " + classWithItem.ToString().ToUpper());
                    if (ply.Class.ClassType.ToString().ToUpper() == classWithItem.ToString().ToUpper())
                    {
                        ply.GiveItem((ItemType)weponId);
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
 