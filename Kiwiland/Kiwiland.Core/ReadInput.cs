using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwiland.Core
{
    public class ReadInput
    {
        String gData;

        public ReadInput(String fileName)
        {
            String[] fileData = null;

            try
            {
                fileData = File.ReadAllLines(fileName);
            }
            catch (FileNotFoundException e)
            {
                //Console.WriteLine("Could not find the File {0}.Exception thrown {1}", fileName, e.Message);
                throw e;
            }

            gData = fileData[0];
        }
        public String GData
        {
            get { return gData; }
        }
    }
}
