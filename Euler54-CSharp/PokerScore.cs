using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parseq;
using Parseq.Combinators;

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

        #region Rank parsers
        
        private static Parser<Card, Rank> ParseNOfAKind(int n, RankType rankType)
        {
            return
                from first in Prims.Any<Card>().Lookahead()
                from tuple in Prims.Satisfy<Card>(card => card.Value == first.Value).Repeat(n)
                select new Rank(rankType, first.Value);
        }
        
        private static Parser<TToken, Unit> DiscardBefore<TToken,T>(Parser<TToken,T> p)
        {
            return Combinator.Choice(p.Lookahead().Ignore(),
                from junk in Prims.Any<TToken>().Ignore()
                from pAhead in DiscardBefore(p)
                select junk);
        }

        private static Parser<TToken,T> FirstAvailable<TToken,T>(Parser<TToken,T> p)
        {
            return
                from junk in DiscardBefore(p)
                from result in p
                select result;
        }

        private static Parser<Card, Rank> ParseOnePair = FirstAvailable(ParseNOfAKind(2, RankType.OnePair));

        private static Parser<Card, Rank> ParseTwoPair =
            from pair1 in ParseOnePair
            from pair2 in ParseOnePair
            select new Rank(RankType.TwoPairs, pair1.HighestCard);

        private static Parser<Card, Rank> ParseThreeOfAKind = FirstAvailable(ParseNOfAKind(3, RankType.ThreeOfAKind));

        #endregion Rank parsers

        public static Rank ComputeBestRank(IEnumerable<Card> hand)
        {
            var handDesc = hand.OrderByDescending(card => card.Value);
            return Combinator.Choice(
                ParseThreeOfAKind,
                ParseTwoPair,
                ParseOnePair)
                .Run(TokenStream.AsStream(handDesc)).Case<Rank>(
                    left: s => new Rank(RankType.HighCard, handDesc.First().Value),
                    right: rank => rank);
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
