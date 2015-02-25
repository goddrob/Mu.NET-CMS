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
        Error
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
    }
}