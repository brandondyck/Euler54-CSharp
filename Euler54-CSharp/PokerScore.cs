﻿using System;
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

        private static Parser<Card, Rank> ParseHighCard =
            from highest in Prims.Any<Card>()
            //from rest in Prims.Any<Card>().Repeat(4)
            //from eof in Prims.EndOfInput<Card>()
            select new Rank(RankType.HighCard, highest.Value);

        private static Parser<Card, Rank> ParsePairAtStart =
            from first in Prims.Any<Card>()
            from second in Prims.Satisfy<Card>(card => card.Value == first.Value)
            select new Rank(RankType.OnePair, first.Value);

        private static Parser<Card, Unit> DiscardBefore(Parser<Card,Rank> p)
        {
            return Combinator.Choice(p.Lookahead().Ignore(),
                from junk in Prims.Any<Card>().Ignore()
                from pAhead in DiscardBefore(p)
                select junk);
        }

        private static Parser<Card, Rank> ParseOnePair =
            from junk in DiscardBefore(ParsePairAtStart)
            from pair in ParsePairAtStart
            //from rest in Prims.Any<Card>().Many0()
            //from eof in Prims.EndOfInput<Card>()
            select pair;

        private static Parser<Card, Rank> ParseTwoPair =
            from pair1 in ParseOnePair
            from pair2 in ParseOnePair
            select new Rank(RankType.TwoPairs, pair1.HighestCard);

        #endregion Rank parsers

        public static Rank ComputeBestRank(IEnumerable<Card> hand)
        {
            var handDesc = hand.OrderByDescending(card => card.Value);
            return Combinator.Choice(
                ParseTwoPair,
                ParseOnePair,
                ParseHighCard)
                .Run(TokenStream.AsStream(handDesc)).Case<Rank>(
                    left: s => throw new Exception("Failed to find best rank"),
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
