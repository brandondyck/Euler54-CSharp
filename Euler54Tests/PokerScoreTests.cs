﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Euler54;

namespace Euler54Tests
{
    [TestClass]
    public class PokerScoreTests
    {
        [TestMethod]
        public void IdenticalHandsAreADraw()
        {
            var player1 = new PokerScore(new Rank(RankType.Flush, Value.T), Value.Three);
            var player2 = new PokerScore(new Rank(RankType.Flush, Value.T), Value.Three);
            Assert.AreEqual(0, player1.CompareTo(player2));
        }

        [TestMethod]
        public void StrongerRankWins()
        {
            var player1 = new PokerScore(new Rank(RankType.Straight, Value.Six), Value.Six);
            var player2 = new PokerScore(new Rank(RankType.HighCard, Value.A), Value.A);
            Assert.IsTrue(player1.CompareTo(player2) > 0);
        }

        [TestMethod]
        public void HighestInRankWinsOnTiedRank()
        {
            var player1 = new PokerScore(new Rank(RankType.Straight, Value.Six), Value.A);
            var player2 = new PokerScore(new Rank(RankType.Straight, Value.T), Value.J);
            Assert.IsTrue(player1.CompareTo(player2) < 0);
        }

        [TestMethod]
        public void HighestInHandBreaksTie()
        {
            var player1 = new PokerScore(new Rank(RankType.FourOfAKind, Value.Five), Value.A);
            var player2 = new PokerScore(new Rank(RankType.FourOfAKind, Value.Five), Value.K);
            Assert.IsTrue(player1.CompareTo(player2) > 0);
        }
    }
}
