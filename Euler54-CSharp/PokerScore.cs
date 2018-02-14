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

        #region Parsing utilities

        private static Parser<Card, Rank> ParseNOfAKind(int n, RankType rankType)
        {
            return
                from first in Prims.Any<Card>().Lookahead()
                from tuple in Prims.Satisfy<Card>(card => card.Value == first.Value).Repeat(n)
                select new Rank(rankType, first.Value);
        }

        private static Parser<TToken, Unit> DiscardBefore<TToken, T>(Parser<TToken, T> p)
        {
            return Combinator.Choice(p.Lookahead().Ignore(),
                from junk in Prims.Any<TToken>().Ignore()
                from pAhead in DiscardBefore(p)
                select junk);
        }

        private static Parser<TToken, T> FirstAvailable<TToken, T>(Parser<TToken, T> p)
        {
            return
                from junk in DiscardBefore(p)
                from result in p
                select result;
        }

        private delegate bool RunningComparison(Card previous, Card current);

        private static Parser<Card, Value> ParseNWithRunningComparison(int n, RunningComparison comparison, Card first, Card previous)
        {
            if (n == 0)
            {
                return Parser.Return<Card, Value>(first.Value);
            }
            return
                from current in Prims.Satisfy<Card>(curr => comparison(previous, curr))
                from rest in ParseNWithRunningComparison(n - 1, comparison, first, current)
                select rest;
            ;
        }

        private static bool AreStraightFlush(Card previous, Card current)
        {
            return current.Suit == previous.Suit && current.Value == previous.Value - 1;
        }

        private static bool AreStraight(Card previous, Card current)
        {
            return current.Value == previous.Value - 1;
        }

        private static Func<Card, bool> ValueOneLessThan(Card compareTo)
        {
            return card => card.Value == compareTo.Value - 1;
        }

        #endregion Parsing utilities

        private static Parser<Card, Rank> ParseOnePair = FirstAvailable(ParseNOfAKind(2, RankType.OnePair));

        private static Parser<Card, Rank> ParseTwoPair =
            from pair1 in ParseOnePair
            from pair2 in ParseOnePair
            select new Rank(RankType.TwoPairs, pair1.HighestCard);

        private static Parser<Card, Rank> ParseThreeOfAKind = FirstAvailable(ParseNOfAKind(3, RankType.ThreeOfAKind));

        private static Parser<Card, Rank> ParseStraight =
            from first in Prims.Any<Card>()
            from rest in ParseNWithRunningComparison(4, AreStraight, first, first)
            select new Rank(RankType.Straight, first.Value);

        private static Parser<Card, Rank> ParseFlush =
            from first in Prims.Any<Card>()
            from rest in Prims.Satisfy<Card>(card => card.Suit == first.Suit).Repeat(4)
            select new Rank(RankType.Flush, first.Value);

        private static Parser<Card, Rank> ParseFullHouse =
            Combinator.Choice(
                from two in ParseNOfAKind(2, RankType.FullHouse)
                from three in ParseNOfAKind(3, RankType.FullHouse)
                select two,

                from three in ParseNOfAKind(3, RankType.FullHouse)
                from two in ParseNOfAKind(2, RankType.FullHouse)
                select three);

        private static Parser<Card, Rank> ParseFourOfAKind = FirstAvailable(ParseNOfAKind(4, RankType.FourOfAKind));

        private static Parser<Card, Rank> ParseStraightFlush =
            from first in Prims.Any<Card>()
            from rest in ParseNWithRunningComparison(4, AreStraightFlush, first, first)
            select new Rank(RankType.StraightFlush, first.Value);

        private static Parser<Card, Rank> ParseRoyalFlush =
            from first in Prims.Satisfy<Card>(card => card.Value == Value.A)
            from rest in ParseNWithRunningComparison(4, AreStraightFlush, first, first)
            select new Rank(RankType.RoyalFlush, first.Value);

        private static (RankType rankType, Parser<Card, Rank> parser)[] RankParsers =
        {
            (RankType.OnePair, ParseOnePair),
            (RankType.TwoPairs, ParseTwoPair),
            (RankType.ThreeOfAKind, ParseThreeOfAKind),
            (RankType.FourOfAKind, ParseFourOfAKind),
            (RankType.FullHouse, ParseFullHouse),
            (RankType.Flush, ParseFlush),
            (RankType.StraightFlush, ParseStraightFlush),
            (RankType.Straight, ParseStraight),
            (RankType.RoyalFlush, ParseRoyalFlush)
        };

        #endregion Rank parsers

        public static Rank ComputeBestRank(IEnumerable<Card> hand)
        {
            var handDesc = hand.OrderByDescending(card => card.Value);
            var orderedParsers =
                from rankAndparser in RankParsers
                orderby rankAndparser.rankType descending
                select rankAndparser.parser;
            return Combinator.Choice(orderedParsers)
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
