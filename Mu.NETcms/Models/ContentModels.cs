using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mu.NETcms.Models
{
    public class WebDbContext : DbContext
    {
        public WebDbContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<NewsPost> News { get; set; }
    }
    [Table("AspNetNews")]
    public class NewsPost
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string HtmlContent { get; set; }
        public int Shares { get; set; }
    }
}