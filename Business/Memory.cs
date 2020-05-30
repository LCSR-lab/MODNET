using System;

namespace Business
{
    public class Memory
    {
        public bool isRAM16X1D(string s)
        {
            if (s == "RAM16X1D")
                return true;
            else return false;
        }

        public bool isRAM32X1D(string s)
        {
            if (s == "RAM32X1D")
                return true;
            else return false;
        }

        public bool isRAM64X1D(string s)
        {
            if (s == "RAM64X1D")
                return true;
            else return false;
        }

        public bool isRAMB16_S36(string s)
        {
            if (s == "RAMB16_S36")
                return true;
            else return false;
        }
    }
}
