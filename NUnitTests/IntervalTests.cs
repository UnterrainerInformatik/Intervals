// *************************************************************************** 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>
// ***************************************************************************

using System;
using Faders;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("Interval")]
    internal class IntervalTests
    {
        private const double EPSILON = 0.00000001;

        [Test]
        public void ConstructingFadersWorks()
        {
            var i = new Interval<int>();
            Assert.IsNotNull(i);
            i = new Interval<int>(0, 0);
            Assert.IsNotNull(i);
            i = new Interval<int>(-1, 101, true, true);
            Assert.IsNotNull(i);

            var l = new Interval<long>();
            Assert.IsNotNull(l);
            l = new Interval<long>(0L, 0L);
            Assert.IsNotNull(l);
            l = new Interval<long>(-1L, 101L, true, true);
            Assert.IsNotNull(l);

            var f = new Interval<float>();
            Assert.IsNotNull(f);
            f = new Interval<float>(0f, 0f);
            Assert.IsNotNull(f);
            f = new Interval<float>(-1f, 101f, true, true);
            Assert.IsNotNull(f);

            var d = new Interval<double>();
            Assert.IsNotNull(d);
            d = new Interval<double>(0d, 0d);
            Assert.IsNotNull(d);
            d = new Interval<double>(-1d, 101d, true, true);
            Assert.IsNotNull(d);
        }

        [Test]
        public void ConstructingFaderWithMinBiggerMaxThrowsException()
        {
            Assert.Throws<ArgumentException>(delegate
            {
                var i = new Interval<int>(100, 0);
            });
        }

        [Test]
        public void ConstructingFaderWithMinBiggerMaxAndExclusiveFlagsThrowsException()
        {
            Assert.Throws<ArgumentException>(delegate
            {
                var i = new Interval<int>(100, 0, false, true);
            });
        }

        [Test]
        public void GrowingMinOverMaxMovesMaxBoundary()
        {
            var i = new Interval<int>(0, 100);

            i.Min = 1000;
            Assert.AreEqual(i.Min, 1000);
            Assert.AreEqual(i.Max, 1000);
        }

        [Test]
        public void ReducingMaxUnderMinMovesMinBoundary()
        {
            var i = new Interval<int>(0, 100);

            i.Max = -100;
            Assert.AreEqual(i.Min, -100);
            Assert.AreEqual(i.Max, -100);
        }

        [Test]
        public void IsMinAndMaxValueInclusiveReturnsRightValue()
        {
            var i = new Interval<int>(0, 100, true, true);
            Assert.AreEqual(i.IsMinValueExclusive, true);
            Assert.AreEqual(i.IsMaxValueExclusive, true);
        }

        [Test]
        public void IsInBetweenReturnsCorrectValueForExclusiveInterval()
        {
            var i = new Interval<int>(0, 100, true, true);
            Assert.AreEqual(i.IsInBetween(50), true);
            Assert.AreEqual(i.IsInBetween(500), false);
            Assert.AreEqual(i.IsInBetween(0), false);
            Assert.AreEqual(i.IsInBetween(100), false);
        }

        [Test]
        public void IsInBetweenReturnsCorrectValueForInclusiveInterval()
        {
            var i = new Interval<int>(0, 100);
            Assert.AreEqual(i.IsInBetween(50), true);
            Assert.AreEqual(i.IsInBetween(500), false);
            Assert.AreEqual(i.IsInBetween(0), true);
            Assert.AreEqual(i.IsInBetween(100), true);
        }
    }
}