using BPCalculator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BPCalculatorUnitTests
{
    [TestClass]
    public class BPCalcTest
    {
        [TestMethod]
        public void TestLowRange()
        {
            BloodPressure bp_calc = new BloodPressure();
            bp_calc.Systolic = 80;
            bp_calc.Diastolic = 50;
            BPCategory bpCategory = bp_calc.Category;
            Assert.AreEqual(bpCategory, BPCategory.Low);
        }
        [TestMethod]
        public void TestNormalRange()
        {
            BloodPressure bp_calc = new BloodPressure();
            bp_calc.Systolic = 100;
            bp_calc.Diastolic = 70;
            BPCategory bpCategory = bp_calc.Category;
            Assert.AreEqual(bpCategory, BPCategory.Normal);
        }
        [TestMethod]
        public void TestPreHighRange()
        {
            BloodPressure bp_calc = new BloodPressure();
            bp_calc.Systolic = 130;
            bp_calc.Diastolic = 85;
            BPCategory bpCategory = bp_calc.Category;
            Assert.AreEqual(bpCategory, BPCategory.PreHigh);
        }
        [TestMethod]
        public void TestHighRange()
        {
            BloodPressure bp_calc = new BloodPressure();
            bp_calc.Systolic = 150;
            bp_calc.Diastolic = 95;
            BPCategory bpCategory = bp_calc.Category;
            Assert.AreEqual(bpCategory, BPCategory.High);
        }
    }
}
