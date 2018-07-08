using System;
using FactorioSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public UIStub CreateUIStub()
        {
            UIStub newStub = new UIStub();
            Controller controller = new Controller(newStub);
            newStub.TextTotalPerSecond.Text = "1";
            newStub.TextMiningProductivity.Text = "40";

            return newStub; 
        }


        /// <summary>
        /// Calculate electronic circuits.
        /// </summary>
        [TestMethod]
        public void Lookup1()
        {
            // Electronic Circuit
            UIStub stub = CreateUIStub();
            stub.TextIngredient.Text = "Electronic Circuit";
            stub.FireCalculate();
            Assert.AreEqual("", stub.TextErrors.Text);
        }

        /// <summary>
        /// Try a bat lookup
        /// </summary>
        [TestMethod]
        public void FailLookup1()
        {
            // Electronic Circuit
            UIStub stub = CreateUIStub();
            stub.TextIngredient.Text = "not an item";
            stub.FireCalculate();
            Assert.IsTrue(stub.TextErrors.Text != "");
        }
    }
}
