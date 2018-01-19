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

using Faders;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("Fader")]
    internal class FaderTests
    {
        private const double EPSILON = 0.00000001;

        [Test]
        public void ConstructingFadersWorks()
        {
            var f = new Fader();
            Assert.IsNotNull(f);
            f = new Fader(2340.0, 2340203450.520534);
            Assert.IsNotNull(f);
            f = new Fader(-2340.0, 23.520534);
            Assert.IsNotNull(f);

            f = new Fader(2340.0, 2340203450.520534, 3000.0);
            Assert.IsNotNull(f);
            f = new Fader(2340.0, 2340203450.520534, -2000.0);
            Assert.IsNotNull(f);
            f = new Fader(2340.0, 2340203450.520534, 999999999999999.520534);
            Assert.IsNotNull(f);

            f = new Fader(2340.0, 2340203450.520534, 0.5, true);
            Assert.IsNotNull(f);
            f = new Fader(2340.0, 2340203450.520534, -0.5, true);
            Assert.IsNotNull(f);
            f = new Fader(2340.0, 2340203450.520534, 1.5, true);
            Assert.IsNotNull(f);
        }
        
        [Test]
        public void ConstructingFaderWithMinBiggerMaxSwapsValues()
        {
            var f = new Fader(100, 0);
            Assert.IsNotNull(f);
            Assert.AreEqual(f.MaxValue, 100, EPSILON);
            Assert.AreEqual(f.MinValue, 0, EPSILON);
        }

        [Test]
        public void SettingWrongValueIsCorrectedByBoundaries()
        {
            var f = new Fader(0, 100) {Value = -50.0};
            Assert.GreaterOrEqual(f.Value, f.MinValue);
            f.Value = 500.0;
            Assert.LessOrEqual(f.Value, f.MaxValue);
        }

        [Test]
        public void GettingAndSettingThePercentageCorrespondsToTheRightValues()
        {
            var f = new Fader(230.0, 280.0) {Value = 260.0};

            Assert.AreEqual(f.Percentage, 0.6, EPSILON);

            f.Percentage = 0.1;
            Assert.AreEqual(f.Value, 235.0, EPSILON);
        }

        [Test]
        public void GettingAndSettingThePercentageOfInvertedFaderCorrespondsToTheRightValues()
        {
            var f = new Fader(0, 100);
            f.IsInverted = true;
            f.Value = 75;

            Assert.AreEqual(f.Percentage, .25, EPSILON);

            f.Percentage = .75;
            Assert.AreEqual(f.Value, 25, EPSILON);
        }

        [Test]
        public void SettingBoundsAfterCounstructionBurstsIntervalCorrectly()
        {
            var f = new Fader(25, 75) {Value = 50};

            f.MinValue = 0;
            Assert.AreEqual(f.MinValue, 0, EPSILON);

            f.MaxValue = 100;
            Assert.AreEqual(f.MaxValue, 100, EPSILON);
        }

        [Test]
        public void ConfiningValueByReducingBoundsChangesValueAccordingly()
        {
            var f = new Fader(0, 100);

            f.Value = 100;
            f.MaxValue = 75;
            Assert.AreEqual(f.Value, 75, EPSILON);

            f.Value = 0;
            f.MinValue = 25;
            Assert.AreEqual(f.Value, 25, EPSILON);
        }

        [Test]
        public void ValueChangedEventIsThrownCorrectly()
        {
            double oldValue = 0;
            var f = new Fader(0, 100);
            f.ValueChanged += (sender, args) => { oldValue = args.OldValue; };

            f.Value = 100;
            Assert.AreEqual(oldValue, 0, EPSILON);
            f.Value = 10;
            Assert.AreEqual(oldValue, 100, EPSILON);
            f.Value = 100;
            Assert.AreEqual(oldValue, 10, EPSILON);
        }

        [Test]
        public void QuadraticValueFadersWork()
        {
            var f = new Fader(20.0, 30.0) {Value = 25.0};
            Assert.IsTrue(f.QuadraticValue.Equals(22.5));
            Assert.AreEqual(f.QuadraticValue, 22.5, EPSILON);

            f.QuadraticValue = 25.0;
            Assert.AreEqual(f.Value, 27.0710678118, EPSILON);
        }

        [Test]
        public void CubicValueFadersWork()
        {
            var f = new Fader(20.0, 30.0) {Value = 25.0};
            Assert.AreEqual(f.CubicValue, 21.25, EPSILON);

            f.CubicValue = 25.0;
            Assert.AreEqual(f.Value, 27.9370052598, EPSILON);
        }

        [Test]
        public void ExponentialValueFadersWork()
        {
            var f = new Fader(20.0, 30.0) {Value = 25.0};
            Assert.AreEqual(f.ExponentialValue, 21.7360679775, EPSILON);

            f.ExponentialValue = 25;
            Assert.AreEqual(f.Value, 28.0043710646, EPSILON);
        }

        [Test]
        public void SlowStartValueFadersWork()
        {
            var f = new Fader(20.0, 30.0) {Value = 21.0};
            Assert.AreEqual(f.BidirectionalSlow, 20.24471741, EPSILON);

            f.BidirectionalSlow = 28.0;
            Assert.AreEqual(f.Value, 27.0483276469, EPSILON);
        }

        [Test]
        public void QuickStartValueFadersWork()
        {
            var f = new Fader(20.0, 30.0) {Value = 21.0};
            Assert.AreEqual(f.BidirectionalQuick, 22.44, EPSILON);

            f.BidirectionalQuick = 28.0;
            Assert.AreEqual(f.Value, 29.2171633333, EPSILON);
        }
    }
}