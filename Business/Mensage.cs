using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class Mensage
    {
        private static string msg;

        public static string my_msg
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}
