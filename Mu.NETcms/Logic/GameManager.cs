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
            int resetLevel = Int32.Parse(ConfigurationManager.AppSettings["ResetLevel"]);
           
            using (var c = new GameDbContext()){
                Character ch = c.Characters.Find(character);
                if (ch.cLevel < resetLevel) return false;
                else if (ch.Money < GetResetCost(ch.Resets)) return false;
                else return true;
            }
            //return false;
        }
        public GameMessageId ResetCharacter(string user, string character)
        {
            if (!IsCharacterOwned(user,character)) return GameMessageId.Error;
            if (IsConnected(user)) return GameMessageId.AccountConnected;
            int resetLevel = Int32.Parse(ConfigurationManager.AppSettings["ResetLevel"]);
            int resetCap = Int32.Parse(ConfigurationManager.AppSettings["ResetMax"]);

            using (var c = new GameDbContext())
            {
                Character ch = c.Characters.Find(character);
                //maybe not needed
                //ch = (Character)c.Entry(ch).GetDatabaseValues().ToObject();
                if (ch.cLevel < resetLevel)
                    return GameMessageId.ResetFailLevel;
                else if (ch.Resets >= resetCap) return GameMessageId.ResetFailCap;
                else if (ch.Money < GetResetCost(ch.Resets)) return GameMessageId.ResetFailZen;
                else
                {
                    //db.Users.Attach(updatedUser);
                    //var entry = db.Entry(updatedUser);
                    //entry.Property(e => e.Email).IsModified = true;
                    //// other changed properties
                    //db.SaveChanges();
                    ch.cLevel = 1;
                    ch.Experience = 0;
                    ch.Money -= GetResetCost(ch.Resets);
                    ch.MapNumber = 0;
                    ch.MapPosX = 182;
                    ch.MapPosY = 128;
                    ch.Resets += 1;
                    c.SaveChanges();
                    //c.Entry(ch).GetDatabaseValues();
                    return GameMessageId.ResetSuccess;
                }

            }
        }
        public int GetResetCost(int resets){
            bool isDynamic = Boolean.Parse(ConfigurationManager.AppSettings["isCostDynamic"]);
            int resetCost = Int32.Parse(ConfigurationManager.AppSettings["ResetCost"]);
            if (!isDynamic) return resetCost;
            else return (resets + 1) * resetCost;
        }
        public ICollection<Character> GetCharsFor(string user)
        {
            using (var c = new GameDbContext())
            {
                return c.Characters.Where(a => a.AccountID == user).ToArray<Character>();
            }
        }
        public bool IsConnected(string user)
        {
            using (var c = new GameDbContext())
            {
                var temp = c.Status.Find(user);
                if (temp != null && temp.ConnectStat == 1) return true;
            }

            return false;
        }
        public Warehouse GetVaultFor(string account)
        {
            using (var c = new GameDbContext())
            {
                return c.Vaults.Find(account);
            }
        }

    }
}