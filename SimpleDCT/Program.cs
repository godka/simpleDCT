using System;
using drizzle.Algorithm;
using System.IO;
using System.Net;
using System.Collections.Generic;
namespace SimpleDCT
{
    class MainClass
    {
        static List<trainelem> trainlist = new List<trainelem>();
        public static void StartParallel(int index)
        {
            trainlist[index].Start();
        }

        public static void Main(string[] args)
        {
            FileStream fs = new FileStream("train-images.idx3-ubyte", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            br.ReadInt32();
            var count = br.ReadInt32();
            var w = br.ReadInt32();
            var h = br.ReadInt32();
            count = IPAddress.HostToNetworkOrder(count);
            w = IPAddress.HostToNetworkOrder(w);
            h = IPAddress.HostToNetworkOrder(h);

            FileStream labelfs = new FileStream("train-labels.idx1-ubyte", FileMode.Open, FileAccess.Read);
            BinaryReader labelbr = new BinaryReader(labelfs);
            labelbr.ReadInt32();labelbr.ReadInt32();
            Console.WriteLine("Init Data");
            double[] double_list = new double[w * h];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < w * h; j++)
                    double_list[j] = br.ReadByte();
                var mat = new Matrix(w, h, double_list);
                var labelwrite = labelbr.ReadChar();
                trainlist.Add(new trainelem(mat, labelwrite));
            }

            Console.WriteLine("Calc Data");
            System.Threading.Tasks.Parallel.For(0, count - 1,
                (i) => { StartParallel(i); });


            Console.WriteLine("Write Data to Disk");
            StreamWriter sw = new StreamWriter("train.txt");
            sw.WriteLine(string.Format("{0} {1} {2}", 28 * 28, 10, 0));
            foreach (var t in trainlist)
            {
                sw.Write(t.ToString());
            }
            sw.Close();
            Console.WriteLine("All Done");
        }
    }
}
