﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public class Card : IEquatable<Card>
    {
        public readonly Suit Suit;
        public readonly Value Value;

        public Card(Value value, Suit suit)
        {
            bool validEnums = Enum.IsDefined(typeof(Value), value)
                && Enum.IsDefined(typeof(Suit), suit);
            if (!validEnums)
            {
                throw new ArgumentException("An enum value is not valid.");
            }
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return String.Format("Card({0}, {1})", Value, Suit);
        }

        public static Card Parse(string s)
        {
            try
            {
                int suitIndex = s.Length - 1;
                string valueString = s.Substring(0, suitIndex);
                string suitString = s.Substring(suitIndex);
                Value value = (Value)Enum.Parse(typeof(Euler54_CSharp.Value), valueString);
                Suit suit = (Suit)Enum.Parse(typeof(Euler54_CSharp.Suit), suitString);
                return new Card(value, suit);
            }
            catch (Exception e)
            {
                throw new ArgumentException("invalid card string: " + s, e);
            }
        }

        public bool Equals(Card other)
        {
            return Suit.Equals(other.Suit) && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Card && ((IEquatable<Card>)obj).Equals(this);
        }
    }
}