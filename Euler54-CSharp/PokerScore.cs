using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler54_CSharp
{
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
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Rank && ((IEquatable<Rank>)obj).Equals(this);
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
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            return obj is PokerScore && ((IEquatable<PokerScore>)obj).Equals(this);
        }

        public override string ToString()
        {
            return String.Join(" ", BestRank, HighestInHand);
        }
    }
}
