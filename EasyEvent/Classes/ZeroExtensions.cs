using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2.API;

namespace ZeroExtensions
{
    public static class Extensions
    {
        /// <summary>
        /// Get the numerical value of item from ItemType
        /// </summary>
        /// <param name="type">The ItemType</param>
        /// <returns>The numerical value of item from ItemType</returns>
        public static int GetValue(this ItemType type)
        {
            switch(type)
            {
                case ItemType.NULL:
                    return -1;
                case ItemType.JANITOR_KEYCARD:
                    return 0;
                case ItemType.SCIENTIST_KEYCARD:
                    return 1;
                case ItemType.MAJOR_SCIENTIST_KEYCARD:
                    return 2;
                case ItemType.ZONE_MANAGER_KEYCARD:
                    return 3;
                case ItemType.GUARD_KEYCARD:
                    return 4;
                case ItemType.SENIOR_GUARD_KEYCARD:
                    return 5;
                case ItemType.CONTAINMENT_ENGINEER_KEYCARD:
                    return 6;
                case ItemType.MTF_LIEUTENANT_KEYCARD:
                    return 7;
                case ItemType.MTF_COMMANDER_KEYCARD:
                    return 8;
                case ItemType.FACILITY_MANAGER_KEYCARD:
                    return 9;
                case ItemType.CHAOS_INSURGENCY_DEVICE:
                    return 10;
                case ItemType.O5_LEVEL_KEYCARD:
                    return 11;
                case ItemType.RADIO:
                    return 12;
                case ItemType.M1911_PISTOL:
                    return 13;
                case ItemType.MEDKIT:
                    return 14;
                case ItemType.FLASHLIGHT:
                    return 15;
                case ItemType.MICROHID:
                    return 16;
                case ItemType.COIN:
                    return 17;
                case ItemType.CUP:
                    return 18;
                case ItemType.AMMOMETER:
                    return 19;
                case ItemType.E11_STANDARD_RIFLE:
                    return 20;
                case ItemType.SBX7_PISTOL:
                    return 21;
                case ItemType.DROPPED_SFA:
                    return 22;
                case ItemType.SKORPION_SMG:
                    return 23;
                case ItemType.LOGICER:
                    return 24;
                case ItemType.POSITRON_GRENADE:
                    return 25;
                case ItemType.SMOKE_GRENADE:
                    return 26;
                case ItemType.DISARMER:
                    return 27;
                case ItemType.DROPPED_RAT:
                    return 28;
                case ItemType.DROPPED_PAT:
                    return 29;
                default:
                    return -1;
            }
        }
        /// <summary> 
        /// Compute the distance between two strings.
        /// </summary>
        /// <param name="s">String to compare</param>
        /// <param name="t">String to compare to</param>
        /// <returns>The number of edits needed to make the string the same</returns>
        public static int LevDistance(this string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
        /// <summary>
        /// Find the closest string in list
        /// </summary>
        /// <param name="compared"> The string to compare.</param>
        /// <param name="list">The list of strings to find the closest match to compared.</param>
        /// <returns>Closest match to compared.</returns>
        public static string ListLevDistance(this string compared, List<String> list)
        {
            int min = Int32.MaxValue-1;
            foreach(string s in list)
            {
                if(min > compared.LevDistance(s))
                {
                    min = compared.LevDistance(s);
                }
            }
            foreach (string s in list)
            {
                if (min == compared.LevDistance(s))
                {
                    return s;
                }
            }
            throw new Exception("This somehow messed up, I do not know why or how, but it messed up. Check ZeroExtensions ListLevDistance");
        }
        /// <summary>
        /// Check if the string is contained in a string that is cointained in a list
        /// </summary>
        /// <param name="compared"> The string to check</param>
        /// <param name="list"> The list of strings to check in.</param>
        /// <returns>If the string is contained in any of the strings in the list</returns>
        public static bool ContainsInList(this string compared, List<String> list)
        {
            foreach(string s in list)
            {
                if(s.Contains(compared))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find the closest string in list that already has the initial string included in it.
        /// </summary>
        /// <param name="compared"> The string to compare.</param>
        /// <param name="list">The list of strings to find the closest match to compared.</param>
        /// <returns>Closest match to compared.Returns null if compared dis not in list.</returns>
        public static string InclusiveListLevDistance(this string compared, List<String> list)
        {
            List<String> temp = new List<string>();
            if(compared.ContainsInList(list))
            {

            }
            else
            {
                return null;
            }

            foreach(string s in list)
            {
                if(s.Contains(compared))
                {
                    temp.Add(s);
                }
                else
                {
                    
                }
            }

            int min = Int32.MaxValue - 1;
            foreach (string s in temp)
            {
                if (min > compared.LevDistance(s))
                {
                    min = compared.LevDistance(s);
                }
            }

            foreach (string s in temp)
            {
                if (min == compared.LevDistance(s))
                {
                    return s;
                }
            }
            return min.ToString();
            //throw new Exception("This somehow messed up, I do not know why or how, but it messed up. Check ZeroExtensions InlcusiveListLevDistance");

        }

        public static void NShuffle<T>(this List<T> list, Random rnd, int n)
        {
            for (var i = 0; i < n; i++)
                Shuffle(list, rnd);
        }

        public static void Shuffle<T>(this List<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count; i++)
                Swap(list, i, rnd.Next(i, list.Count));
        }

        public static void Swap<T>(List<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }



}
