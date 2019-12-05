using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sign_Up.Models
{
    public class UserDataModel
    {
        private TESTEntities db = new TESTEntities();

        public string account { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }
        public string city { get; set; }
        public string village { get; set; }
        public string address { get; set; }

        public Int32 Find_id()
        {
            // 失敗
            //var id = db.TUSER.Max(o => o != null ? o.id : 0);

            // 如果null就給0，else Max
            var id = (db.TUSER.Select(s => s.id)).DefaultIfEmpty(0).Max();
            return id + 1;
        }

        public void Create_USER(TUSER tuser)
        {
            db.TUSER.Add(tuser);
            db.SaveChanges();
        }
        public bool Check_USER(string account,string password)
        {
            var data = db.TUSER.Where(o => o.account == account && o.password == password).Select(s => s).ToList();
            if (data.Count > 0)
                return true;
            else
                return false;
        }
    }
}