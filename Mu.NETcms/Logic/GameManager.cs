using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            return true;
        }
        public bool IsCharacterReset(string character)
        {
            return true;
        }
        public bool ResetCharacter(string character)
        {
            return true;
        }
    }
}