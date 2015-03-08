using Mu.NETcms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;

namespace Mu.NETcms.Logic
{
    public class GameCache
    {
        public static Dictionary<RankType,List<Rank_Char>> cache_c_rank;
        public static List<Rank_Guild> cache_g_rank;
        public static List<NewsPost> cache_news;
        public static DateTime cache_g_rank_date;
        public static DateTime cache_c_rank_date;
        public const int CACHE_UPDATE = 600;
        public const int CACHE_RATE_I = 60;
        public static ServerStats cache_servstat;
        public static void ReCache(bool c_rank, bool g_rank, bool news){
            if (g_rank)
            {
                cache_g_rank = new List<Rank_Guild>();
                using (var context = new GameDbContext())
                {
                    List<Guild> guilds = context.Guilds.ToList();
                    foreach (var guild in guilds)
                    {
                        Rank_Guild rguild = new Rank_Guild();
                        rguild.Name = guild.G_Name;
                        rguild.Master = guild.G_Master;
                        rguild.Mark = guild.G_Mark;
                        rguild.Members = 0;
                        int reset_count = 0;
                        foreach (var gm in context.GuildMembers.Where(m => m.G_Name == rguild.Name).ToList())
                        {
                            rguild.Members += 1;
                            int temp = context.Characters.Find(gm.Name).Resets;
                            reset_count += temp;
                        }
                        rguild.Resets = reset_count;
                        cache_g_rank.Add(rguild);
                    }
                }
                cache_g_rank_date = DateTime.Now;
                cache_g_rank.Sort();
            }
            if (c_rank)
            {
                if (cache_c_rank == null) cache_c_rank = new Dictionary<RankType, List<Rank_Char>>();
                foreach (RankType rt in Enum.GetValues(typeof(RankType)))
                {
                    cache_c_rank[rt] = GetRankCharFromDB(rt);
                }
                cache_c_rank_date = DateTime.Now;
            }
            if (news)
            {
                if (cache_news == null) cache_news = new List<NewsPost>();
                using (var c = new WebDbContext())
                {
                    cache_news = c.News.OrderByDescending(m => m.Date).ToList();
                }
            }
        }

        public static ServerStats GetServerStats()
        {
            if (cache_servstat == null || DateTime.Now.Subtract(cache_servstat.lastUpdated).TotalSeconds > CACHE_RATE_I)
            {
                cache_servstat = new ServerStats();
                cache_servstat.Version = "Season 6 Episode 3";
                using (var context = new GameDbContext())
                {
                    cache_servstat.TotalAccounts = context.Accounts.Count();
                    cache_servstat.TotalChars = context.Characters.Count();
                    cache_servstat.OnlineAccounts = context.Status.Count(s => s.ConnectStat != 0);
                }
                cache_servstat.lastUpdated = DateTime.Now;
            }
            //else if (DateTime.Now.Subtract(cache_servstat.lastUpdated).TotalSeconds > CACHE_RATE_I)
            //{

            //}
            return cache_servstat;
        }
        public static GSInfo GetInfoGS(int code)
        {
            if (code == 0) return new GSInfo() {Code = 0, ExpRate = 25, Name = "Aegis-PVP", MaxOnline = 200 };
            return new GSInfo() { Code = 1, ExpRate = 20, Name = "Aegis-Non-PVP", MaxOnline = 100 };
        }
        public static int[] GetAllGS()
        {
            return new int[] { 0, 1 };
        }

        public static List<NewsPost> GetServerNews()
        {
            if (cache_news == null) GameCache.ReCache(false, false, true);
            return cache_news;
        }
        public static NewsPost GetPostById(int id)
        {
            if (cache_news == null) GameCache.ReCache(false, false, true);
            return cache_news.Find(n => n.Id == id);
            //using (var c = new WebDbContext())
            //{
            //    return c.News.Single(n => n.Id == id);
            //}
        }
        #region Ranking
        public static List<Rank_Char> TopChar(int num)
        {
            return GetRankChar(RankType.ALL_CHAR).Take(num).ToList();
        }
        public static List<Rank_Guild> TopGuild(int num)
        {
            return GetRankGuild().Take(num).ToList();
        }
        public static List<Rank_Char> GetRankChar(RankType type)
        {
            if (cache_c_rank == null || DateTime.Now.Subtract(cache_c_rank_date).TotalSeconds > CACHE_UPDATE) GameCache.ReCache(true, false, false);
            return cache_c_rank[type];
        }
        private static List<Rank_Char> GetRankCharFromDB (RankType type){
            //Check Cache

            List<Rank_Char> rank = new List<Rank_Char>();
            if (type == RankType.ALL_CHAR)
            {
                using (var c = new GameDbContext())
                {
                    foreach (var ch in c.Characters.OrderByDescending(cr => cr.GrandResets).ThenByDescending(cr => cr.Resets).ThenByDescending(cr => cr.cLevel).Take(100))
                    {
                        rank.Add(new Rank_Char(ch));
                    }
                }
                return rank;
            }
            int low,high;
            switch (type){
                case RankType.BK_CHAR: 
                    low = 16;
                    high = 19;
                    break;
                case RankType.SM_CHAR:
                    low = 0;
                    high = 3;
                    break;
                case RankType.ELF_CHAR:
                    low = 32;
                    high = 35;
                    break;
                case RankType.SUM_CHAR:
                    low = 80;
                    high = 83;
                    break;
                case RankType.MG_CHAR:
                    low = 48;
                    high = 50;
                    break;
                case RankType.DL_CHAR:
                    low = 64;
                    high = 66;
                    break;
                case RankType.RF_CHAR:
                    low = 96;
                    high = 98;
                    break;
                default:
                    return null;
            }

            using (var c = new GameDbContext())
            {
                foreach (var ch in c.Characters.Where(cr => cr.Class >= low && cr.Class <= high).OrderByDescending(cr => cr.GrandResets).ThenByDescending(cr => cr.Resets).ThenByDescending(cr => cr.cLevel).Take(100))
                {
                    rank.Add(new Rank_Char(ch));
                }
            }
            return rank;
        }

        public static List<Rank_Guild> GetRankGuild()
        {
            if (cache_g_rank == null || DateTime.Now.Subtract(cache_g_rank_date).TotalSeconds > CACHE_UPDATE) GameCache.ReCache(false,true,false);
            return cache_g_rank;
        }





    }
    public enum RankType
    {
        ALL_CHAR,
        BK_CHAR,
        SM_CHAR,
        ELF_CHAR,
        SUM_CHAR,
        MG_CHAR,
        DL_CHAR,
        RF_CHAR
    }
    public class Rank_Guild : IComparable<Rank_Guild>
    {
        public string Name { get; set; }
        public Byte[] Mark { get; set; }
        public string Master { get; set; }
        public int Resets { get; set; }
        public int Members { get; set; }

        public int CompareTo(Rank_Guild other)
        {
            if (Resets > other.Resets) return -1;
            else if (Resets < other.Resets) return 1;
            else return 0;
        }
    }
    public class Rank_Char
    {
        public Rank_Char(Character c)
        {
            Name = c.Name;
            Class = c.Class;
            Level = c.cLevel;
            STR = c.Strength;
            AGI = c.Dexterity;
            VIT = c.Vitality;
            ENG = c.Energy;
            Map = c.MapNumber;
            Resets = c.Resets;
            GrandResets = c.GrandResets;
        }
        public Rank_Char()
        {
        }
        public string Name { get; set; }
        public int Class { get; set; }
        public int Level { get; set; }
        public int STR { get; set; }
        public int AGI { get; set; }
        public int VIT { get; set; }
        public int ENG { get; set; }
        public int Map { get; set; }
        public int Resets { get; set; }
        public int GrandResets { get; set; }

    }
        #endregion
    public class GSInfo
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int ExpRate { get; set; }
        public int MaxOnline { get; set; }
    }
    public class ServerStats
    {
        public DateTime lastUpdated { get; set; }
        public string Version { get; set; }
        public int TotalAccounts { get; set; }
        public int TotalChars { get; set; }
        public int OnlineAccounts { get; set; }
    }
}