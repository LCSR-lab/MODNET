using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Business
{
    public class Analysis
    {
        Logic logic = new Logic();
        Maintenance maintenance = new Maintenance();

        public int injection_analysis(string path, string filename, string outputpath)
        {
            string errtype = "SET"; //Types of Injection Fault: SET, SEU, RAMB
            int i = 0;
            int j = 0;
            string line = "";
            string new_file = "";
            string module_name = "";
            int mod_count;
            string analysis = "";
            int counter = 0;
            string timescale = "";

            //Console.Write("reading netlist for " + filename + "...");
            

            System.IO.StreamReader net_file = new System.IO.StreamReader(path + filename + ".v");
            timescale = net_file.ReadLine();
            //Console.WriteLine("[Done]!!");
            //Console.Write("reading module name... ");

            module_name = net_file.ReadLine();
            //Console.WriteLine("[Done]!!");
            //Console.Write("reading module ports... ");

            string file = net_file.ReadToEnd();
            i = 0;
            j = file.IndexOf(")");
            System.IO.File.WriteAllText(path + "temp" + ".txt", file);
            char[] listport = new char[j + 4];
            net_file.Close();
            net_file = new System.IO.StreamReader(path + "temp" + ".txt");
            net_file.ReadBlock(listport, 0, j + 4);
            string module_ports = new string(listport);
            //Console.WriteLine("[Done]!!");
            //Console.Write("reading module port declarations... ");

            i = j + 5;
            j = file.IndexOf("wire");
            char[] decport = new char[j - i];
            net_file.ReadBlock(decport, 0, j - i);
            string module_dec = new string(decport);
            //Console.WriteLine("[Done]!!");
            //Console.Write("reading module wires... ");

            i = j;
            j = file.LastIndexOf("wire ");
            char[] wireport = new char[j - i];
            net_file.ReadBlock(wireport, 0, j - i);
            string module_wire = new string(wireport);
            net_file.Close();
            file = file.Replace(module_ports + module_dec + module_wire, "");
            System.IO.File.WriteAllText(path + "temp" + ".txt", file);
            net_file = new System.IO.StreamReader(path + "temp" + ".txt");
            net_file.ReadLine();
            module_wire += net_file.ReadLine() + "\n";
            //Console.WriteLine("[Done]!!");
            // Console.WriteLine(module_wire); Texto do Alexandre
            new_file = net_file.ReadToEnd();
            net_file.Close();
            //Console.WriteLine("Analyzing submodules for " + filename + "... ");
            System.IO.File.WriteAllText(path + filename + "1.txt", new_file);
            System.IO.StreamReader n_file = new System.IO.StreamReader(path + filename + "1.txt");

            while ((line = n_file.ReadLine()) != null)
            {

                if (line.Contains(" (") && (logic.iscombinational(AnalyseLine(line))) && errtype == "SET")
                {
                    analysis += line.Replace(AnalyseLine(line), AnalyseLine(line) + "_mod") + "\n .inj(inj[" + (counter).ToString() + "]) ,\n";
                    counter++;
                }
                else if (line.Contains(" (") && (logic.issequential(AnalyseLine(line))) && errtype == "SEU")
                {
                    analysis += line.Replace(AnalyseLine(line), AnalyseLine(line) + "_mod") + "\n .inj(inj[" + (counter).ToString() + "]) ,\n";
                    counter++;
                }
                
                else if (line.Contains(" (") && File.Exists(path + AnalyseLine(line) + ".v") && (errtype == "SEU" || errtype == "SET"))
                {
                    mod_count = injection_analysis(path, AnalyseLine(line), outputpath);
                    if (mod_count != 0)
                    {
                        analysis += AnalyseLine(line) + " " + AnalyseLine(line) + "_uut (\n .inj(inj[" + (counter + mod_count - 1).ToString() + " : " + counter + "]),\n";
                        counter += mod_count;
                    }
                    else
                    {
                        analysis += AnalyseLine(line) + " " + AnalyseLine(line) + "_uut (\n";
                        counter += mod_count;
                    }
                }
                else if (line.Contains(" (") && File.Exists(path + AnalyseLine(line) + ".v") && errtype == "RAMB")
                {
                    mod_count = injection_analysis(path, AnalyseLine(line), outputpath);
                    if (mod_count != 0)
                    {
                        analysis += AnalyseLine(line) + " " + AnalyseLine(line) + "_uut (\n .inj(inj[" + (counter + mod_count - 1).ToString() + " : " + counter + "]),\n.data_mask(data_mask),\n.address(address),\n";
                        counter += mod_count;
                    }
                    else
                    {
                        analysis += AnalyseLine(line) + " " + AnalyseLine(line) + "_uut (\n";
                        counter += mod_count;
                    }
                }
                else if (line.Contains("defparam")) analysis += line + "\n";
                else if (line.Contains("//")) analysis += line + "\n";
                else if (line.Contains(");")) analysis += line + "\n";
                else if (line.Contains(".")) analysis += line + "\n";
                else analysis += line;
            }

            n_file.Close();
            maintenance.deletefile(path, filename + "1");
            maintenance.deletefile(path, "temp");
            System.IO.Directory.CreateDirectory(outputpath);
            string temp1 = "";

            if (errtype == "SEU" || errtype == "SET")
            {
                if (counter != 0)
                    temp1 = module_name + "\n inj,\n" + module_ports + "\ninput [" + (counter - 1).ToString() + ": 0] inj ;\n" + module_dec + module_wire + "\n" + analysis;
                else temp1 = module_name + "\n inj,\n" + module_ports + "\ninput inj ;\n" + module_dec + module_wire + "\n" + analysis;

                System.IO.File.WriteAllText(outputpath + filename + ".v", temp1);
            }

            if (errtype == "RAMB")
            {
                if (counter != 0)
                    temp1 = module_name + "\n inj,\ndata_mask,\naddress,\n" + module_ports + "\ninput [" + (counter - 1).ToString() + ": 0] inj ;\ninput [35:0] data_mask;\ninput [8:0] address;\n" + module_dec + module_wire + "\n" + analysis;
                else temp1 = module_name + "\n inj,\ndata_mask,\naddress,\n" + module_ports + "\ninput inj ;\ninput [35:0] data_mask;\ninput [8:0] address;\n" + module_dec + module_wire + "\n" + analysis;

                System.IO.File.WriteAllText(outputpath + filename + ".v", temp1);
            }
            //Console.WriteLine("[Done Analysis for " + filename + "]!!");
            return counter;
        }

        public string AnalyseLine(string line)
        {
            string[] words = line.Split(' ');
            return words[2];
        }

        public string secondword(string line)
        {
            string[] words = line.Split(' ');
            return words[1];
        }
    }
}
