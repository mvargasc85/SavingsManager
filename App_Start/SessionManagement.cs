using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SavingsManager
{
    public class SessionManagement
    {
        public int UserID
        {
            get
            {
                return HttpContext.Current.Session["UserID"] == null ? 0 :
              (int)HttpContext.Current.Session["UserID"];
            }
            set { HttpContext.Current.Session["UserID"] = value; }
        }
        public string UserType
        {
            get
            {
                return HttpContext.Current.Session["UserType"] == null ?
      string.Empty : HttpContext.Current.Session["UserType"].ToString();
            }
            set { HttpContext.Current.Session["UserType"] = value; }
        }
        public decimal UserCurrentBalance
        {
            get
            {
                return HttpContext.Current.Session["UserCurrentBalance"] == null ?
      0 : (decimal)HttpContext.Current.Session["UserCurrentBalance"];
            }
            set { HttpContext.Current.Session["UserCurrentBalance"] = value; }
        }
    }
}