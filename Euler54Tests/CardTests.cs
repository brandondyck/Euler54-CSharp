using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Euler54;

namespace Euler54Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void Parse10DGiveCardTenD()
        {
            Card parsed = Card.Parse("TD");
            Assert.AreEqual(new Card(Value.T, Suit.D), parsed);
        }

        [TestMethod]
        public void Parse2HGiveCardTwoH()
        {
            Card parsed = Card.Parse("2H");
            Assert.AreEqual<Card>(new Card(Value.Two, Suit.H), parsed);
        }

        [TestMethod]
        public void ParseQSGiveCardQS()
        {
            Card parsed = Card.Parse("QS");
            Assert.AreEqual<Card>(new Card(Value.Q, Suit.S), parsed);
        }

        [TestMethod]
        public void ParseXPThrows()
        {
            Assert.ThrowsException<ArgumentException>(() => Card.Parse("XP"));
        }
        
        [TestMethod]
        public void ParseHandsGivesCorrectCards()
        {
            string s = "5S 4D JS 3D 8H 6C TS 3S AD 8C";
            Card[] player1Expected =
            {
                new Card(Value.Five, Suit.S),
                new Card(Value.Four, Suit.D),
                new Card(Value.J, Suit.S),
                new Card(Value.Three, Suit.D),
                new Card(Value.Eight, Suit.H)
            };
            Card[] player2Expected =
            {
                new Card(Value.Six, Suit.C),
                new Card(Value.T, Suit.S),
                new Card(Value.Three, Suit.S),
                new Card(Value.A, Suit.D),
                new Card(Value.Eight, Suit.C)
            };

            var (player1Actual, player2Actual) = Card.ParseHands(s);
            Assert.IsTrue(player1Expected.SequenceEqual(player1Actual));
            Assert.IsTrue(player2Expected.SequenceEqual(player2Actual));
        }
    }
}
