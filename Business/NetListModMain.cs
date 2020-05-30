using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Dto;

namespace Business
{
    public class NetListModMain
    {
       // public string msg;

        //set the path of the netlist
        private string path = "C:\\NetListmod\\analysis\\";
        private string outputpath = "C:\\NetListmod\\analysis\\output\\";
        private string srcpath = "C:\\NetListmod\\analysis\\src\\";
        private string datespath = "C:\\NetListmod\\analysis\\date\\";

        Maintenance maintenance = new Maintenance();
        Analysis analysis = new Analysis();

        public string NetListMain()
        {
            //Limpar variável inicial
            Mensage.my_msg = "";
           
            System.IO.Directory.CreateDirectory(outputpath);
            System.IO.Directory.CreateDirectory(srcpath);
            System.IO.Directory.CreateDirectory(datespath);
            string[] files;
            string netlist = "dut"; //netlist name (Desing Under Test --> Target)
            string filename = "dut"; //top module name (Desing Under Test --> Target)
            string temp;
            string timescale = "";
            string netfile = System.IO.File.ReadAllText(path + netlist + ".vm");
            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;
            files = Directory.GetFiles(srcpath);
            if (files.Length < 2) //Changed 1 to 2 by  Alexandre Coelho 
            {
                //Console.Write("generating modules from netlist..."); //generating modules from netlist
                Mensage.my_msg = "generating modules from netlist... ";
                 
                i = netfile.IndexOf("`timescale");
                j = netfile.IndexOf("module ");
                for (k = i; k < j; k++)
                    timescale += netfile[k];

                do
                {
                    i = netfile.IndexOf("module ");
                    j = netfile.IndexOf("endmodule ");
                    char[] currentmodule = new char[j];
                    char[] currentmodule_n = new char[j - i];
                    System.IO.StreamReader n_file = new System.IO.StreamReader(path + netlist + ".vm");
                    n_file.ReadBlock(currentmodule, 0, j);
                    n_file.Close();

                    for (k = i; k < j; k++)
                        currentmodule_n[k - i] = currentmodule[k];

                    temp = new string(currentmodule);
                    string currentmodule_s = new string(currentmodule_n);
                    netfile = netfile.Replace(temp, "");
                    i = netfile.IndexOf("endmodule ");
                    j = netfile.IndexOf("*/");
                    temp = "";

                    for (k = i; k < j; k++)
                        temp += netfile[k];

                    currentmodule_s += temp + "*/";
                    netfile = netfile.Replace(temp + "*/", "");
                    temp = "";
                    System.IO.File.WriteAllText(srcpath + analysis.secondword(currentmodule_s) + ".v", timescale + currentmodule_s);
                    System.IO.File.WriteAllText(path + netlist + ".vm", netfile);
                    m++;
                } while (netfile.IndexOf("endmodule ") >= 0);
                //Console.WriteLine("Done!!!");

                Mensage.my_msg = Mensage.my_msg + "Done!!!\r\n";
            }
            maintenance.deletefile(srcpath, "temp");
            analysis.injection_analysis(srcpath, filename, outputpath);
            //Console.WriteLine("Done!!!");

            Mensage.my_msg = Mensage.my_msg + "Done!!!\r\n";

            //Salvando o Log
            WriteFile();
            
            //Enviando para a View
            return Mensage.my_msg;
        }

        private void WriteFile()
        {

            //string datespath = @"C:\NetListmod\analysis\date\";
            //System.IO.Directory.CreateDirectory(datespath);

            //string file = @"C:\NetListmod\analysis\Date\NetListLog.txt";
            string file = datespath + "NetListLog.txt";

            //Declaração do método StreamWriter passando o caminho e nome do arquivo que deve ser salvo
            StreamWriter writer = new StreamWriter(file);

            //Writing in the file 
            writer.Write(Mensage.my_msg);

            //Close the file
            writer.Close();

            //Clean the reference to memory
            writer.Dispose();
        }

        public void ConfigPath(PathTransfer pathTransfer)
        {
            this.path = pathTransfer.myPath;
            this.outputpath = pathTransfer.myOutput;
            this.srcpath = pathTransfer.mySrc;
            this.datespath = pathTransfer.myDate;
        }
    }
}
