using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mu.NETcms.Models;
using System.Configuration;

namespace Mu.NETcms.Logic
{
    public class GameManager
    {
        public static GameManager Create()
        {
            return new GameManager();
        }
        public bool IsCharacterOwned(string user, string character)
        {
            using (var c = new GameDbContext())
            {
                AccountCharacter ac = c.AccountsEx.Find(user);
                if (ac.GameID1.Equals(character) || ac.GameID2.Equals(character) || ac.GameID3.Equals(character)
                    || ac.GameID4.Equals(character) || ac.GameID5.Equals(character)) return true;
            }
            return false;
        }
        public bool IsCharacterReset(string character)
        {
            bool isDynamic = Boolean.Parse(ConfigurationManager.AppSettings["isCostDynamic"]);
            int resetLevel = Int32.Parse(ConfigurationManager.AppSettings["ResetLevel"]);
            int resetCost = Int32.Parse(ConfigurationManager.AppSettings["ResetCost"]);
            
            using (var c = new GameDbContext()){
                Character ch = c.Characters.Find(character);
                if (ch.cLevel < resetLevel) return false;
                else if (isDynamic && ch.Money < (resetCost * ch.Resets)) return false;
                else if (!isDynamic && ch.Money < resetCost) return false;
                else return true;
            }
            //return false;
        }
        public bool ResetCharacter(string character)
        {
            return true;
        }
    }
}