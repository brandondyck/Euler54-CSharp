using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler54_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new PokerScore(new Rank(RankType.FullHouse, Value.J), Value.Ten));
        }
    }
}
