using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Engine {
    public class BeoordelingsEngine : BeoordelingProject.Engine.IBeoordelingsEngine {
        public BeoordelingsEngine() {
        }

        public double deelaspect(double midden, int weging) {
            return midden * weging;
        }

        public double totaalDeelaspect(List<double> middens, List<int> wegingen) {
            double totaal = 0;

            if (wegingen.Count > 1) {
                for (int i = 0; i < middens.Count; i++) {
                    totaal += deelaspect(middens[i], wegingen[i]);
                }
            }
            else {
                foreach (double midden in middens) {
                    totaal += deelaspect(midden, wegingen[0]);
                }
            }

            return totaal;
        }

        public int totaalWeging(List<int> wegingen) {
            int totaal = 0;

            foreach (int weging in wegingen) {
                totaal += weging;
            }

            return totaal;
        }

        public double totaalScore(List<double> middens, List<int> wegingen) {
            double totaal = 0;

            if (totaalWeging(wegingen) == 0) {
                return totaal;
            }

            totaal = totaalDeelaspect(middens, wegingen) / totaalWeging(wegingen);

            return totaal;
        }
    }
}