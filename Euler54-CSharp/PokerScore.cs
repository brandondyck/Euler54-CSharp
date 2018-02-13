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

    public enum RankType
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

    public struct Rank : IEquatable<Rank>, IComparable<Rank>
    {
        public readonly RankType Type;
        public readonly Value HighestCard;

        public Rank(RankType type, Value highestCard)
        {
            bool validEnums = Enum.IsDefined(typeof(RankType), type)
                && Enum.IsDefined(typeof(Value), highestCard);
            if (!validEnums)
            {
                throw new ArgumentException("An enum value is not valid.");
            }

            Type = type;
            HighestCard = highestCard;
        }

        public int CompareTo(Rank other)
        {
            int cmpType = Type.CompareTo(other.Type);
            if (cmpType != 0)
            {
                return cmpType;
            }
            return HighestCard.CompareTo(other.HighestCard);
        }

        public bool Equals(Rank other)
        {
            return HighestCard == other.HighestCard && Type == other.Type;
        }

        public override string ToString()
        {
            return String.Join(" ", Type, HighestCard);
        }
    }

    public struct PokerScore : IEquatable<PokerScore>, IComparable<PokerScore>
    {
        public readonly Rank BestRank;
        public readonly Value HighestInHand;

        public PokerScore(Rank bestRank, Value highestInHand)
        {
            if (!Enum.IsDefined(typeof(Value), highestInHand))
            {
                throw new ArgumentException("An enum value is not valid.");
            }

            BestRank = bestRank;
            HighestInHand = highestInHand;
        }

        public int CompareTo(PokerScore other)
        {
            int cmpBestRank = BestRank.CompareTo(other.BestRank);
            if (cmpBestRank != 0)
            {
                return cmpBestRank;
            }
            return HighestInHand.CompareTo(other.HighestInHand);
        }

        public bool Equals(PokerScore other)
        {
            return BestRank.Equals(other.BestRank)
                && HighestInHand == other.HighestInHand;
        }

        public override string ToString()
        {
            return String.Join(" ", BestRank, HighestInHand);
        }
    }
}
