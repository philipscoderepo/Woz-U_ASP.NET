using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "1,2,3,";
            string[] ss = s.Split(',');
            for(int i=0; i<ss.Length; i++)
            {
                Console.WriteLine(ss[i]);
            }
            
            Console.ReadLine();
        }
    }
}
