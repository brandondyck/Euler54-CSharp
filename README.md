# Euler54

Solution to [Project Euler Problem 54](https://projecteuler.net/problem=54) in C#.

## Rationale

It turns out that we can approach poker scoring as a parsing problem. If we [abuse LINQ syntax a little bit](https://mikehadlow.blogspot.com/2011/01/monads-in-c-4-linq-loves-monads.html), we can express most of the work in a handful of elegant LINQ queries using [monadic parser combinators](https://blogs.msdn.microsoft.com/lukeh/2007/08/19/monadic-parser-combinators-using-c-3-0/).

## Bugs

This would be much more concise in Haskell.

## License

The file `p054_poker.txt` is taken from  [Project Euler Problem 54](https://projecteuler.net/problem=54) and is licensed under [Creative Commons Licence:
Attribution-NonCommercial-ShareAlike 2.0 UK: England & Wales](http://creativecommons.org/licenses/by-nc-sa/2.0/uk/).

Copyright 2018 Brandon Dyck <<brandon@dyck.us>>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
