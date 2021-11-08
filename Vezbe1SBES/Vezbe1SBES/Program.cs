using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezbe1SBES
{
    class Program
    {
        static void Main(string[] args)
        {

            ServerUp server = new ServerUp();

            Console.WriteLine("Server succesfully up");
            Console.ReadKey(true);
        }
    }
}
