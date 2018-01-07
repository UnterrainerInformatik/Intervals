// *************************************************************************** 
//  Copyright (c) 2013 by Unterrainer Informatik OG.
//  This source is licensed to Unterrainer Informatik OG.
//  All rights reserved.
//  
//  In other words:
//  YOU MUST NOT COPY, USE, CHANGE OR REDISTRIBUTE ANY ART, MUSIC, CODE OR
//  OTHER DATA, CONTAINED WITHIN THESE DIRECTORIES WITHOUT THE EXPRESS
//  PERMISSION OF Unterrainer Informatik OG.
// 
//  Classes using other, less restrictive licenses are explicitly marked.
// ---------------------------------------------------------------------------
//  Programmer: , 
//  Created: 2013-11-26
// ***************************************************************************

using NUnit.Framework;

namespace Intervals.NUnitTests
{
    /// <summary>
    ///     Tests for the Fader class.
    /// </summary>
    [TestFixture]
    [Category("Fader")]
    internal class FaderTests
    {
        private const double EPSILON = 0.00000001;

        /// <summary>
        ///     Tests the constructors of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestConstructorsFaders()
        {
            Fader f;

            f = new Fader();
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

        /// <summary>
        ///     Tests the max and min boundaries of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestBoundryValueFaders()
        {
            Fader f = new Fader(0, 100);
            f.Value = -50.0;
            Assert.GreaterOrEqual(f.Value, f.MinValue);
            f.Value = 500.0;
            Assert.LessOrEqual(f.Value, f.MaxValue);
        }

        /// <summary>
        ///     Tests the percentage property of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestPercentageValueFaders()
        {
            Fader f = new Fader(230.0, 280.0);
            f.Value = 260.0;

            Assert.AreEqual(f.Percentage, 0.6, EPSILON);

            f.Percentage = 0.1;
            Assert.AreEqual(f.Value, 235.0, EPSILON);
        }

        /// <summary>
        ///     Tests the QuadraticValue property of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestQuadraticValueFaders()
        {
            Fader f = new Fader(20.0, 30.0);
            f.Value = 25.0;
            Assert.IsTrue(f.QuadraticValue.Equals(22.5));
            Assert.AreEqual(f.QuadraticValue, 22.5, EPSILON);

            f.QuadraticValue = 25.0;
            Assert.AreEqual(f.Value, 27.0710678118, EPSILON);
        }

        /// <summary>
        ///     Tests the CubicValue property of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestCubicValueFaders()
        {
            Fader f = new Fader(20.0, 30.0);
            f.Value = 25.0;
            Assert.AreEqual(f.CubicValue, 21.25, EPSILON);

            f.CubicValue = 25.0;
            Assert.AreEqual(f.Value, 27.9370052598, EPSILON);
        }


        /// <summary>
        ///     Tests the ExponentialValue property of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestExponentialValueFaders()
        {
            Fader f = new Fader(20.0, 30.0);
            f.Value = 25.0;
            Assert.AreEqual(f.ExponentialValue, 21.7360679775, EPSILON);

            f.ExponentialValue = 25;
            Assert.AreEqual(f.Value, 28.0043710646, EPSILON);
        }

        /// <summary>
        ///     Tests the SlowStart property of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestSlowStartValueFaders()
        {
            Fader f = new Fader(20.0, 30.0);
            f.Value = 21.0;
            Assert.AreEqual(f.BidirectionalSlow, 20.24471741, EPSILON);

            f.BidirectionalSlow = 28.0;
            Assert.AreEqual(f.Value, 27.0483276469, EPSILON);
        }

        /// <summary>
        ///     Tests the Quick property of the fader class.
        /// </summary>
        [Test]
        [Category("Utils.Fader")]
        public void TestQuickStartValueFaders()
        {
            Fader f = new Fader(20.0, 30.0);
            f.Value = 21.0;
            Assert.AreEqual(f.BidirectionalQuick, 22.44, EPSILON);

            f.BidirectionalQuick = 28.0;
            Assert.AreEqual(f.Value, 29.2171633333, EPSILON);
        }
    }
}