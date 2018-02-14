using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler54_CSharp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("usage: euler54 <filename>");
                
                return 1;
            }

            int wins = File.ReadLines(args[0])
                .Select(Card.ParseHands)
                .Select(hands => (PokerScore.Compute(hands.Item1), PokerScore.Compute(hands.Item2)))
                .Where(scores => scores.Item1.CompareTo(scores.Item2) > 0)
                .Count();
            Console.WriteLine(wins);
            return 0;
        }
    }
}
