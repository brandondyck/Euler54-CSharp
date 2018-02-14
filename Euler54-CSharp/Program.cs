using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler54
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("usage: euler54 <filename>");
                return 1;
            }

            try
            {
                int wins = File.ReadLines(args[0])
                    .Select(Card.ParseHands)
                    .Select(hands => (PokerScore.Compute(hands.Item1), PokerScore.Compute(hands.Item2)))
                    .Where(scores => scores.Item1.CompareTo(scores.Item2) > 0)
                    .Count();
                Console.WriteLine(wins);
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return 2;
            }
        }
    }
}
