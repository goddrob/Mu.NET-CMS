using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mu.NETcms.Logic
{
    public class ItemManager
    {
        public static List<Item> VaultToList(byte[] vault){
            List<Item> list = new List<Item>();
            int len = vault.Length;
            int lts = 16;
            for (int i = 0; i < len; i = i + lts)
            {
                byte[] temp = new byte[lts];
                if (len < i + lts)
                {
                    lts = len - i;
                }
                Array.Copy(vault, i, temp, 0, lts);
                string hex_tmp = ByteArrayToString(temp);
                list.Add(Item.CreateFromHex(ByteArrayToString(temp)));
            }
            return list;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }

    public class Item
    {
        public bool isEmpty { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public int Durability { get; set; }
        public int Option { get; set; }
        public bool HasLuck { get; set; }
        public bool HasSkill { get; set; }
        public int ExcelentCode { get; set; }
        public int AncientCode { get; set; }
        public string Serial { get; set; }
        public int Refinery { get; set; }
        public int HarmonyType { get; set; }
        public int HarmonyValue { get; set; }
        public List<string> ExcOptions { get; set; }
        public static List<string> DecodeExcOpt(int code)
        {
            int rcode = code;
            if (code > 1) rcode = Math.Abs(code - 64);
            List<string> options = new List<string>();
            if (rcode >= 64)
            {
                options.Add("Test");
                rcode -= 64;
            }
            if (rcode >= 32)
            {
                options.Add("HP");
                rcode -= 32;
            }
            if (rcode >= 16)
            {
                options.Add("Mana");
                rcode -= 16;
            }
            if (rcode >= 8)
            {
                options.Add("DD");
                rcode -= 8;
            }
            if (rcode >= 4)
            {
                options.Add("Ref");
                rcode -= 4;
            }
            if (rcode >= 2)
            {
                options.Add("DSR");
                rcode -= 2;
            }
            if (rcode >= 1)
            {
                options.Add("Zen");
                rcode -= 1;
            }

            return options;
        }
        public static Item CreateFromHex(string hex){
            Item item = new Item();

            if (hex.Equals("ffffffffffffffffffffffffffffffff"))
            {
                item.isEmpty = true;
                return item;
            }
            else item.isEmpty = false;
            // Id
            item.Id = Convert.ToInt32(hex.Substring(0, 2), 16);

            //Level,skill,luck,add
            int var_opt = Convert.ToInt32(hex.Substring(2, 2), 16);
            if (var_opt > 127)
            {
                item.HasSkill = true;
                var_opt -= 128;
            }
            else item.HasSkill = false;
            item.Level = (int)(var_opt / 8);
            var_opt -= item.Level * 8;
            if (var_opt > 3)
            {
                item.HasLuck = true;
                var_opt -= 4;
            }
            else item.HasLuck = false;
            item.Option = var_opt;
            //Durability
            item.Durability = Convert.ToInt32(hex.Substring(4, 2), 16);
            //Serial
            item.Serial = hex.Substring(6, 8);
            //Excelent
            item.ExcelentCode = Convert.ToInt32(hex.Substring(14, 2), 16);
            if (item.ExcelentCode > 0) item.ExcOptions = Item.DecodeExcOpt(item.ExcelentCode);
            else item.ExcOptions = new List<string>();
            //Ancient
            item.AncientCode = Convert.ToInt32(hex.Substring(16, 2), 16);
            //Type
            item.Type = Convert.ToInt32(hex.Substring(18, 1), 16);
            //Refinery
            item.Refinery = Convert.ToInt32(hex.Substring(19, 1), 16);
            //Harmony
            item.HarmonyType = Convert.ToInt32(hex.Substring(20, 1), 16);
            item.HarmonyValue = Convert.ToInt32(hex.Substring(21, 1), 16);
            return item;
        }
    }
}