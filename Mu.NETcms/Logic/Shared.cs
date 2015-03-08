using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mu.NETcms.Logic
{
    
    public enum GameMessageId
    {
        ResetSuccess,
        ResetFailZen,
        ResetFailLevel,
        ResetFailCap,
        AccountConnected,
        UnstuckSucces,
        Error
    }
    public class GMarkUtil
    {
        public static string ToTableString(Byte[] g_mark,int size)
        {
            string str = BitConverter.ToString(g_mark).Replace("-", "");
            string result = @"<table style=""width:"+8*size+@"px;height:"+8*size+@"px;margin:auto;"" border=0 cellpadding=0 cellspacing=0><tr>";
            int count = 0;
            foreach (char c in str)
            {
                count++;
                result += @"<td style=""background-color:"+CharToColor(c)+@";"" width=""" + size + @""" height=""" + size + @"""></td>";
                if (count % 8 == 0)
                {
                    result += @"</tr>";
                    if (count != 64) result += @"<tr>";
                } 
            }
            result += "</table>";

            return result;
        }
        private static string CharToColor(char c)
        {
            switch (char.ToLower(c))
            {
                /*#fe0000'; $color[5]='#ff8a00'; $color[6]='#ffff00'; $color[7]='#8cff01'; 
                 * $color[8]='#00ff00'; $color[9]='#01ff8d'; $color['a']='#00ffff'; $color['b']='#008aff'; 
                 * $color['c']='#0000fe'; $color['d']='#8c00ff'; $color['e']='#ff00fe'; $color['f']='#ff008c'; 
                 * */
                case '0': return " ";
                case '1': return "#000000";
                case '2': return "#8c8a8d";
                case '3': return "#ffffff";
                case '4': return "#fe0000";
                case '5': return "#ff8a00";
                case '6': return "#ffff00";
                case '7': return "#8cff01";
                case '8': return "#00ff00";
                case '9': return "#01ff8d";
                case 'a': return "#00ffff";
                case 'b': return "#008aff";
                case 'c': return "#0000fe";
                case 'd': return "#8c00ff";
                case 'e': return "#ff00fe";
                case 'f': return "#ff008c";
                default:
                    return " ";
            }
        }
    }
    public class MapTranslator
    {
        static public string ToString(int code)
        {
            string var =
                code == 0 ? "Lorencia"
                : code == 1 ? "Dungeon"
                : code == 2 ? "Devias"
                : code == 3 ? "Noria"
                : code == 4 ? "LostTower"
                : code == 5 ? "Exile"
                : code == 6 ? "Arena"
                : code == 7 ? "Atlans"
                : code == 8 ? "Tarkan"
                : (code == 9 || code == 32) ? "Event Map" // DS
                : code == 10 ? "Icarus"
                : ((code >= 11 && code <= 17) || (code == 52)) ? "Event Map" // BC
                : ((code >= 18 && code <= 23) || (code == 53)) ? "Event Map" // CC
                : ((code >= 24 && code <= 29) || (code == 36)) ? "Kalima"
                : code == 30 ? "Valley of Loren"
                : code == 31 ? "Land of Trials"
                : code == 33 ? "Aida"
                : code == 34 ? "Crywolf"
                : code == 37 ? "Kanturu"
                : code == 38 ? "Kanturu 2"
                : code == 39 ? "Kanturu 3"
                : code == 40 ? "Silent Map"
                : code == 41 ? "Barracks"
                : code == 42 ? "Refuge"
                : (code >= 45 && code <= 50) ? "Event Map" // IT
                : code == 51 ? "Elbeland"
                : code == 56 ? "Swamp of Calmness"
                : (code == 57 || code == 58) ? "Raklion"
                : code == 62 ? "Santa's Village"
                : (code == 63 || code == 64) ? "Vulcanus"
                : (code >= 65 && code <= 68) ? "Event Map" // Double Goer
                : (code >= 69 && code <= 72) ? "Event Map" // Imperial Guardian
                : (code == 80 || code == 81) ? "Kalrutan"
                : "Unknown";
            return var;
        }
    }
    public class ClassTranslator
    {
        static public string ToString(int code)
        {
            string var =
                code == 0 ? "Dark Wizard"
                : code == 1 ? "Soul Master"
                : code == 3 ? "Grand Master"
                : code == 16 ? "Dark Knight"
                : code == 17 ? "Blade Knight"
                : code == 19 ? "Blade Master"
                : code == 32 ? "Elf"
                : code == 33 ? "Muse Elf"
                : code == 35 ? "High Elf"
                : code == 48 ? "Magic Gladiator"
                : code == 50 ? "Duel Master"
                : code == 64 ? "Dark Lord"
                : code == 66 ? "Lord Emperor"
                : code == 80 ? "Summoner"
                : code == 81 ? "Bloody Summoner"
                : code == 83 ? "Dimension Master"
                : code == 96 ? "Rage Fighter"
                : code == 98 ? "Fist Master"
                : "Unknown";
            return var;
        }
        static public string ToShortString(int code)
        {
            string var =
                code == 0 ? "DW"
                : code == 1 ? "SM"
                : code == 3 ? "GrM"
                : code == 16 ? "DK"
                : code == 17 ? "BK"
                : code == 19 ? "BM"
                : code == 32 ? "Elf"
                : code == 33 ? "ME"
                : code == 35 ? "HE"
                : code == 48 ? "MG"
                : code == 50 ? "DM"
                : code == 64 ? "DL"
                : code == 66 ? "LE"
                : code == 80 ? "Sum"
                : code == 81 ? "BS"
                : code == 83 ? "DiM"
                : code == 96 ? "RF"
                : code == 98 ? "FM"
                : "Unknown";
            return var;
        }
    }
}