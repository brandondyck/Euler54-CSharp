using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler54_CSharp
{
    public enum Suit { H, D, C, S }

    public enum Value
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        J = 11,
        Q = 12,
        K = 13,
        A = 14
    }

    public enum Rank
    {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public struct PokerScore : IEquatable<PokerScore>, IComparable<PokerScore>
    {
        public readonly Rank BestRank;
        public readonly Value HighestInRank;
        public readonly Value HighestInHand;

        public PokerScore(Rank bestRank, Value highestInRank, Value highestInHand)
        {
            bool validEnums = Enum.IsDefined(typeof(Rank), bestRank)
                && Enum.IsDefined(typeof(Value), highestInRank)
                && Enum.IsDefined(typeof(Value), highestInHand);
            if (!validEnums)
            {
                throw new ArgumentException("An enum value is not valid.");
            }
            BestRank = bestRank;
            HighestInRank = highestInRank;
            HighestInHand = highestInHand;
        }

        public int CompareTo(PokerScore other)
        {
            int cmpBestRank = BestRank.CompareTo(other.BestRank);
            if (cmpBestRank != 0)
            {
                return cmpBestRank;
            }

            int cmpHighestInRank = HighestInRank.CompareTo(other.HighestInRank);
            if (cmpHighestInRank != 0)
            {
                return cmpHighestInRank;
            }

            return HighestInHand.CompareTo(other.HighestInHand);
        }

        public bool Equals(PokerScore other)
        {
            return BestRank == other.BestRank
                && HighestInRank == other.HighestInRank
                && HighestInHand == other.HighestInHand;
        }

        public override string ToString()
        {
            return String.Join(" ", BestRank, HighestInRank, HighestInHand);
        }
    }
}
