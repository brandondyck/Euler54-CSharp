using System;
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
        public void HighestCardCorrect()
        {
            assertHandRank("8C TS KC 9H 4S", RankType.HighCard, Value.K);
        }

        [TestMethod]
        public void OnePairCorrectWhenHighestValue()
        {
            assertHandRank("8C TS KC KH 4S", RankType.OnePair, Value.K);
        }

        [TestMethod]
        public void OnePairCorrectWhenNotHighestValue()
        {
            assertHandRank("8C TS KC TH 4S", RankType.OnePair, Value.T);
        }

        [TestMethod]
        public void TwoPairCorrectWithJunkBetween()
        {
            assertHandRank("8C TS KC KH 8S", RankType.TwoPairs, Value.K);
        }

        [TestMethod]
        public void TwoPairCorrectWithJunkBefore()
        {
            assertHandRank("4C TS KC TH 4D", RankType.TwoPairs, Value.T);
        }

        [TestMethod]
        public void TwoPairCorrectWithJunkAfter()
        {
            assertHandRank("4C TS 2C TH 4D", RankType.TwoPairs, Value.T);
        }
    }
}
