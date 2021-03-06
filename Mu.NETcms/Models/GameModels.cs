﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mu.NETcms.Models
{

    public class GameDbContext : DbContext
    {
        public GameDbContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountCharacter> AccountsEx { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<AccountStat> Status { get; set; }
        public DbSet<Warehouse> Vaults { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<GuildMember> GuildMembers { get; set; }
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
    [Table("AccountCharacter")]
    public class AccountCharacter
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        [Key]
        public string Id { get; set; }
        public string GameID1 { get; set; }
        public string GameID2 { get; set; }
        public string GameID3 { get; set; }
        public string GameID4 { get; set; }
        public string GameID5 { get; set; }
        public string GameIDC { get; set; }
    }
    [Table("Character")]
    public class Character
    {
        public string AccountID { get; set; }
        [Key]
        public string Name { get; set; }
        public int cLevel { get; set; }
        public int LevelUpPoint { get; set; }
        public Byte Class { get; set; }
        public int Experience { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Vitality { get; set; }
        public int Energy { get; set; }
        public Int16 MapNumber { get; set; }
        public Int16 MapPosX { get; set; }
        public Int16 MapPosY { get; set; }
        public Byte CtlCode { get; set; }
        public int Resets { get; set; }
        public int GrandResets { get; set; }
        public int Money { get; set; }


    }
    [Table("MEMB_STAT")]
    public class AccountStat
    {
        [Key]
        public string memb___id { get; set; }
        public Byte ConnectStat { get; set; }
        public string ServerName { get; set; }
        public string IP { get; set; }
        public DateTime ConnectTM { get; set; }
        public DateTime DisConnectTM { get; set; }
    }
    [Table("warehouse")]
    public class Warehouse
    {
        [Key]
        public string AccountID { get; set; }
        public Byte[] Items { get; set; }
        public int Money { get; set; }
    }
    [Table("Guild")]
    public class Guild
    {
        [Key]
        public string G_Name { get; set; }
        public Byte[] G_Mark { get; set; }
        public string G_Master { get; set; }
    }
    [Table("GuildMember")]
    public class GuildMember
    {
        [Key]
        public string Name { get; set; }
        public string G_Name { get; set; }
    }

}