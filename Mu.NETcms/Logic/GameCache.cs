using Mu.NETcms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mu.NETcms.Logic
{
    public class GameCache
    {
        public static ServerInfo GetServerInfo(int code)
        {
            if (code == 0) return new ServerInfo() {Code = 0, ExpRate = 25, Name = "Aegis-PVP", MaxOnline = 200 };
            return new ServerInfo() { Code = 1, ExpRate = 20, Name = "Aegis-Non-PVP", MaxOnline = 100 };
        }
        public static int[] GetAvailableServers()
        {
            return new int[] { 0, 1 };
        }

        public static List<NewsPost> GetServerNews()
        {
            using (var c = new WebDbContext())
            {
                return c.News.OrderByDescending( m => m.Date).ToList();
            }
        }
        public static NewsPost GetPostById(int id)
        {
            using (var c = new WebDbContext())
            {
                return c.News.Single(n => n.Id == id);
            }
        }




    }
    public class ServerInfo
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int ExpRate { get; set; }
        public int MaxOnline { get; set; }
    }
}