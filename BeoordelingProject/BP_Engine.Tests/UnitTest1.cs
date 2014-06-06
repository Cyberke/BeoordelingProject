using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeoordelingProject.Engine;
using System.Collections.Generic;

namespace BeoordelingProject.Tests {
    [TestClass]
    public class BeoordelingsEngineUT {
        List<double> middens = new List<double>();
        List<int> wegingen = new List<int>();
        IBeoordelingsEngine engine = new BeoordelingsEngine();
        double midden1 = 12.5; // RV
        double midden2 = 8; // OV
        double res1, res2;

        public BeoordelingsEngineUT() {
            wegingen.Add(3);
            wegingen.Add(3);
            wegingen.Add(2);
            wegingen.Add(2);
            wegingen.Add(1);
            wegingen.Add(2);
            wegingen.Add(3);
        }

        [TestMethod]
        public void deelaspectTest() {
            res1 = engine.deelaspect(midden1, wegingen[0]);
            res2 = engine.deelaspect(midden2, wegingen[1]);

            Assert.AreEqual(res1, 37.5);
            Assert.AreEqual(res2, 24);
        }

        [TestMethod]
        public void totaalDeelaspectTest() {
            middens.Add(midden1);
            middens.Add(midden2);

            double res = engine.totaalDeelaspect(middens, wegingen);

            Assert.AreEqual(res, 61.5);
        }

        [TestMethod]
        public void totaalWegingTest() {
            double res = engine.totaalWeging(wegingen);

            Assert.AreEqual(res, 16);
        }

        [TestMethod]
        public void totaalScore() {
            middens.Add(midden1);
            middens.Add(midden2);

            double res = engine.totaalScore(middens, wegingen);

            Assert.AreEqual(res, 3.8438);
        }
    }
}
