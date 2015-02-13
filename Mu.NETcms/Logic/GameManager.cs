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
                if (ac.GameID1 != null && ac.GameID1.Equals(character)) return true;
                if (ac.GameID2 != null && ac.GameID2.Equals(character)) return true;
                if (ac.GameID3 != null && ac.GameID3.Equals(character)) return true;
                if (ac.GameID4 != null && ac.GameID4.Equals(character)) return true;
                if (ac.GameID5 != null && ac.GameID5.Equals(character)) return true;
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
        public GameMessageId ResetCharacter(string user, string character)
        {
            if (!IsCharacterOwned(user,character)) return GameMessageId.Error;

            bool isDynamic = Boolean.Parse(ConfigurationManager.AppSettings["isCostDynamic"]);
            int resetLevel = Int32.Parse(ConfigurationManager.AppSettings["ResetLevel"]);
            int resetCost = Int32.Parse(ConfigurationManager.AppSettings["ResetCost"]);
            int resetCap = Int32.Parse(ConfigurationManager.AppSettings["ResetMax"]);

            using (var c = new GameDbContext())
            {
                Character ch = c.Characters.Find(character);
                //maybe not needed
                //ch = (Character)c.Entry(ch).GetDatabaseValues().ToObject();
                if (ch.cLevel < resetLevel)
                    return GameMessageId.ResetFailLevel;
                else if (ch.Resets >= resetCap) return GameMessageId.ResetFailCap;
                else if ((isDynamic && ch.Money < (resetCost * ch.Resets))
                    || (!isDynamic && ch.Money < resetCost)) return GameMessageId.ResetFailZen;
                else
                {
                    //TO-DO: reset
                    return GameMessageId.ResetSuccess;
                }

            }
        }
        public ICollection<Character> getCharsFor(string user)
        {
            using (var c = new GameDbContext())
            {
                return c.Characters.Where(a => a.AccountID == user).ToArray<Character>();
            }
        }
    }
}