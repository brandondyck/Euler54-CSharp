using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Euler54_CSharp;

namespace Euler54Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void Parse10DGiveCardTenD()
        {
            Card parsed = Card.Parse("10D");
            Assert.AreEqual(new Card(Value.Ten, Suit.D), parsed);
        }

        [TestMethod]
        public void Parse2HGiveCardTwoH()
        {
            Card parsed = Card.Parse("2H");
            Assert.AreEqual<Card>(new Card(Value.Two, Suit.H), parsed);
        }
    }
}
