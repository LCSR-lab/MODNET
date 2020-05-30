using System;

namespace Business
{
    public class Maintenance
    {
        public void deletefile(string path, string name)
        {
            if (System.IO.File.Exists(path + name + ".txt"))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.

                try
                {
                    System.IO.File.Delete(path + name + ".txt");
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
    }
}
