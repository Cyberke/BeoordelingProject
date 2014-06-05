using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeoordelingProject.Engine;
using System.Collections.Generic;
using BeoordelingProject.Helpers;

namespace BeoordelingProject.Tests {
    [TestClass]
    public class BeoordelingsEngineUT {
        Dictionary<string, double> beoordelingsgraden = new Dictionary<string, double>();
        Dictionary<string, double> keuzes = new Dictionary<string, double>();
        List<int> wegingen = new List<int>();
        IBeoordelingsEngine engine = new BeoordelingsEngine();
        KeyValuePair<string, double> keuze1 = new KeyValuePair<string, double>();
        KeyValuePair<string, double> keuze2 = new KeyValuePair<string, double>();
        double res1, res2;

        public BeoordelingsEngineUT() {
            beoordelingsgraden = engine.getBeoordelingsgraden();

            wegingen.Add(3);
            wegingen.Add(3);
            wegingen.Add(2);
            wegingen.Add(2);
            wegingen.Add(1);
            wegingen.Add(2);
            wegingen.Add(3);

            keuze1 = KVPHelper.GetEntry(beoordelingsgraden, "RV");
            keuze2 = KVPHelper.GetEntry(beoordelingsgraden, "OV");
        }

        [TestMethod]
        public void deelaspectTest() {
            res1 = engine.deelaspect(keuze1, wegingen[0]);
            res2 = engine.deelaspect(keuze2, wegingen[1]);

            Assert.AreEqual(res1, 37.5);
            Assert.AreEqual(res2, 24);
        }

        [TestMethod]
        public void totaalDeelaspectTest() {
            keuzes.Add(keuze1.Key, res1);
            keuzes.Add(keuze2.Key, res2);

            double res = engine.totaalDeelaspect(keuzes, wegingen);

            Assert.AreEqual(res, 61.5);
        }

        [TestMethod]
        public void totaalWegingTest() {
            double res = engine.totaalWeging(wegingen);

            Assert.AreEqual(res, 16);
        }

        [TestMethod]
        public void totaalScore() {
            res1 = engine.deelaspect(keuze1, wegingen[0]);
            res2 = engine.deelaspect(keuze2, wegingen[1]);

            keuzes.Add(keuze1.Key, res1);
            keuzes.Add(keuze2.Key, res2);

            double res = engine.totaalScore(keuzes, wegingen);

            Assert.AreEqual(res, 3.8438);
        }
    }
}
