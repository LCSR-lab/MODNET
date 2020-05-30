using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Logic
    {
        public bool iscombinational(string s)
        {
            //add all the combinational gates
            if (s == "LUT4_L" || s == "LUT5" || s == "LUT5_L" || s == "LUT6" || s == "LUT6_L" || s == "INV" || s == "LUT3" || s == "LUT2" || s == "LUT4" || s == "MUXF5" || s == "MUXF8" || s == "MUXF7" || s == "LUT3_L" || s == "BUFGP")
                return true;
            else return false;
        }

        public bool issequential(string s)
        {
            //add all FFs
            if ((s == "FDD" || s == "FDE" || s == "FDC_1" || s == "FDC" || s == "FDCE" || s == "FD" || s == "FDPE" || s == "FDP" || s == "FDRE" || s == "FDRSE" || s == "FDR" || s == "FDS" || s == "FDSE" || s == "FDRS"))
                return true;
            else return false;
        }
    }
}
