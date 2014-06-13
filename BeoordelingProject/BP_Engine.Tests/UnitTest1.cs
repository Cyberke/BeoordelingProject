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
        public void totaalScoreTest() {
            middens.Add(midden1);
            middens.Add(midden2);

            double res = engine.totaalScore(middens, wegingen);
            double afronding = Math.Round(res, 4);

            Assert.AreEqual(afronding, 3.8438);
        }

        // Volgende testen zijn gebaseerd op "Rekenblad bij eindbeoordeling BP 2013-2014 BAKO.xlsx"
        // Eindbeoordelingen hebben een weging over een volledig aspect.
        // Tussentijdse beoordelingen hebben over elke deelaspect een weging. (zie boven)

        [TestMethod]
        public void eindScorePromotorTest() {
            // Eindbeoordeling waarbij alleen de promotor op 1 aspect beoordeelt.
            // Voorbeeld: Attitudes
            // Score op 40

            wegingen = new List<int>() {
                2
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Deze voorbeeld: Max = (3x 20) = 60 op 40 = 1.5
            double eindRes = (resPromotor) / 1.5;

            Assert.AreEqual(eindRes, 16);
        }

        [TestMethod]
        public void eindScorePromotorTweedelezerTest() {
            // Eindbeoordeling waarbij de promotor en tweede lezer op 1 aspect beoordelen.
            // Voorbeeld: Vormtechnische aspecten
            // Score op 40

            wegingen = new List<int>() {
                2
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // 2de lezer (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Deze voorbeeld: Max = (6x 20) = 120 op 40 = 3
            double eindRes = (resPromotor + resTweedelezer) / 3;

            Assert.AreEqual(eindRes, 16);
        }

        [TestMethod]
        public void eindScorePromotorTweedelezerCriticalFriendTest() {
            // Eindbeoordeling waarbij de promotor, tweede lezer en critical friend (cf) op 1 aspect beoordelen.
            // Voorbeeld 1: Inhoud en opbouw argumentatie
            // Voorbeeld 2: Praktische relevantie en/of realisaties
            // Beide scores op 60

            wegingen = new List<int>() {
                3
            };

            // Voorbeeld 1: Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor1 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 2: Promotor (2x OV)
            middens.Add(10.5);
            middens.Add(12.5);

            double resPromotor2 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 1: 2de lezer (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer1 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 2: 2de lezer (2x OV)
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer2 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 1: Critical Friend (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resCriticalFriend1 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 2: Critical Friend (2x OV)
            middens.Add(12.5);
            middens.Add(10.5);

            double resCriticalFriend2 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Voorbeeld 1: Max = (9x 20) = 180 op 60 = 3
            // Voorbeeld 2: Max = (6x 20) = 120 op 60 = 2
            double eindRes1 = (resPromotor1 + resTweedelezer1 + resCriticalFriend1) / 3;
            double eindRes2 = (resPromotor2 + resTweedelezer2 + resCriticalFriend2) / 2;

            double afronding1 = Math.Ceiling(eindRes1);
            double afronding2 = Math.Ceiling(eindRes2);

            Assert.AreEqual(afronding1, 24);
            Assert.AreEqual(afronding2, 31);
        }

        [TestMethod]
        public void testTussentijds()
        {
            List<double> scores = new List<double>();
            List<int> wegingen = new List<int>();

            scores.Add(14.5);
            scores.Add(12.5);
            scores.Add(18);
            scores.Add(18);
            scores.Add(10.5);
            scores.Add(12.5);
            scores.Add(8);

            wegingen.Add(3);
            wegingen.Add(3);
            wegingen.Add(2);
            wegingen.Add(2);
            wegingen.Add(1);
            wegingen.Add(2);
            wegingen.Add(3);

            double totaal = engine.totaalScore(scores, wegingen);
            Assert.IsNotNull(totaal);
        }

        [TestMethod]
        public void eindresultaatTest() {
            // Eindbeoordeling waarbij alleen de promotor op 1 aspect beoordeelt.
            // Voorbeeld: Attitudes
            // Score op 40

            wegingen = new List<int>() {
                2
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Deze voorbeeld: Max = (3x 20) = 60 op 40 = 1.5
            double eindResAttides = (resPromotor) / 1.5;

            // Eindbeoordeling waarbij de promotor en tweede lezer op 1 aspect beoordelen.
            // Voorbeeld: Vormtechnische aspecten
            // Score op 40

            wegingen = new List<int>() {
                2
            };

            // Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor1 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // 2de lezer (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Deze voorbeeld: Max = (6x 20) = 120 op 40 = 3
            double eindResVormtechnischeAspecten = (resPromotor + resTweedelezer) / 3;

            // Eindbeoordeling waarbij de promotor, tweede lezer en critical friend (cf) op 1 aspect beoordelen.
            // Voorbeeld 1: Inhoud en opbouw argumentatie
            // Voorbeeld 2: Praktische relevantie en/of realisaties
            // Beide scores op 60

            wegingen = new List<int>() {
                3
            };

            // Voorbeeld 1: Promotor (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resPromotor2 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 2: Promotor (2x OV)
            middens.Add(10.5);
            middens.Add(12.5);

            double resPromotor3 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 1: 2de lezer (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer1 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 2: 2de lezer (2x OV)
            middens.Add(8);
            middens.Add(8);

            double resTweedelezer2 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 1: Critical Friend (3x OV)
            middens.Add(8);
            middens.Add(8);
            middens.Add(8);

            double resCriticalFriend1 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Voorbeeld 2: Critical Friend (2x OV)
            middens.Add(12.5);
            middens.Add(10.5);

            double resCriticalFriend2 = engine.totaalScore(middens, wegingen);

            middens.Clear();

            // Deler wordt berekend door het maximum aantal punten te delen door de herleden punt.
            // Voorbeeld 1: Max = (9x 20) = 180 op 60 = 3
            // Voorbeeld 2: Max = (6x 20) = 120 op 60 = 2
            double eindRes1 = (resPromotor1 + resTweedelezer1 + resCriticalFriend1) / 3;
            double eindRes2 = (resPromotor2 + resTweedelezer2 + resCriticalFriend2) / 2;

            double afronding1 = Math.Ceiling(eindRes1);
            double afronding2 = Math.Ceiling(eindRes2);

            double totaalDerTotalen = Math.Ceiling((eindResAttides + eindResVormtechnischeAspecten + afronding1 + afronding2) / 10);

            Assert.AreEqual(totaalDerTotalen, 9);
        }
    }
}
