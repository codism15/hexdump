using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexDump
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];

            byte[] buf = new byte[16];

            using (var ins = File.OpenRead(filename))
            {
                long offset = 0;

                while (true)
                {
                    int bytes = ins.Read(buf, 0, buf.Length);
                    if (bytes == 0) break;

                    // write offset
                    Console.Write(string.Format("{0:x8} ", offset));

                    // write in hex mode
                    Console.Write(string.Join(" ", buf.Take(bytes).Select(b => b.ToString("x2"))));

                    if (bytes < buf.Length)
                    {
                        Console.Write(new string(' ', 3 * (buf.Length - bytes)));
                    }

                    Console.Write("  ");
                    // write in ASC mode
                    Console.Write(new string(buf.Take(bytes).Select(b => (char)b).Select(c => Char.IsControl(c) ? '.' : c).ToArray()));
                    
                    Console.WriteLine();
                    offset += buf.Length;
                }
            }

            //Console.Write("press anykey to quit");
            //Console.ReadKey();
        }
    }
}
