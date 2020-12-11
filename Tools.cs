using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PromoDH
{
    public class Tools
    {
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            return "Data Source=190.210.162.183;Initial Catalog=PromoTopline2020;Persist Security Info=True;User ID=PromoTopline2020;Password=PromoTopline2020";
        }

    }
}
