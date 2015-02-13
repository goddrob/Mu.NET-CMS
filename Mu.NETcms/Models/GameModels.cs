using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mu.NETcms.Models
{
    public class GameModels
    {
        public class GameDbContext : DbContext
        {
            public GameDbContext()
                : base("DefaultConnection")
            {
                
            }
            public DbSet<Account> Accounts { get; set; }
        }
        [Table("MEMB_INFO")]
        public class Account
        {
            
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int memb_guid { get; set; }
            [Key]
            public string memb___id { get; set; }
            public string memb__pwd { get; set; }
            public string mail_addr { get; set; }
            public string memb_name { get; set; }
            public string sno__numb { get; set; }
            public string bloc_code { get; set; }
            public string ctl1_code { get; set; }
        }
    }
}