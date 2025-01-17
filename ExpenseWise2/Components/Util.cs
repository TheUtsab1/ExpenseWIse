using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWise2.Components
{
    internal class Util
    {
        public static string ROOTFOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExpenseWise");
        public static string TAG = Path.Combine(ROOTFOLDER, "tag.json");
        public static string TRANSACTION = Path.Combine(ROOTFOLDER, "transaction.json");

        public static bool ISAUTHENTICATED = false;
    }
}
