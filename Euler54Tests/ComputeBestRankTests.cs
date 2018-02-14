﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Euler54_CSharp;

namespace Euler54Tests
{
    [TestClass]
    public class ComputeBestRankTests
    {
        private void assertHandRank(string hand, RankType rankType, Value highestCardInRank)
        {
            var cards = Card.ParseHand(hand);
            Rank expectedRank = new Rank(rankType, highestCardInRank);
            Rank actualRank = Rank.ComputeBestRank(cards);
            Assert.AreEqual(expectedRank, actualRank);
        }

        [TestMethod]
        public void HighCard()
        {
            assertHandRank("8C TS KC 9H 4S", RankType.HighCard, Value.K);
        }

        [TestMethod]
        public void OnePairWhenHighestValue()
        {
            assertHandRank("8C TS KC KH 4S", RankType.OnePair, Value.K);
        }

        [TestMethod]
        public void OnePairWhenNotHighestValue()
        {
            assertHandRank("8C TS KC TH 4S", RankType.OnePair, Value.T);
        }

        [TestMethod]
        public void TwoPairWithJunkBetween()
        {
            assertHandRank("8C TS KC KH 8S", RankType.TwoPairs, Value.K);
        }

        [TestMethod]
        public void TwoPairWithJunkBefore()
        {
            assertHandRank("4C TS KC TH 4D", RankType.TwoPairs, Value.T);
        }

        [TestMethod]
        public void TwoPairWithJunkAfter()
        {
            assertHandRank("4C TS 2C TH 4D", RankType.TwoPairs, Value.T);
        }

        [TestMethod]
        public void ThreeOfAKindBeforeJunk()
        {
            assertHandRank("5H TC TD TS 7S", RankType.ThreeOfAKind, Value.T);
        }

        [TestMethod]
        public void ThreeOfAKindBetweenJunk()
        {
            assertHandRank("5H TC TD TS QS", RankType.ThreeOfAKind, Value.T);
        }

        [TestMethod]
        public void ThreeOfAKindAfterJunk()
        {
            assertHandRank("KH TC TD TS AS", RankType.ThreeOfAKind, Value.T);
        }

        [TestMethod]
        public void Straight()
        {
            assertHandRank("2H 3C 5D 4H 6D", RankType.Straight, Value.Six);
        }

        [TestMethod]
        public void Flush()
        {
            assertHandRank("2H 5H 7H 9H JH", RankType.Flush, Value.J);
        }
    }
}
