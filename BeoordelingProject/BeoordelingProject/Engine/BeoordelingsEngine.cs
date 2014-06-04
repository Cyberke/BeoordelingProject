using BeoordelingProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Engine {
    public class BeoordelingsEngine : BeoordelingProject.Engine.IBeoordelingsEngine {
        Dictionary<string, double> beoordelingsgraden = new Dictionary<string, double>();

        public Dictionary<string, double> getBeoordelingsgraden() {
            return beoordelingsgraden;
        }
        
        public BeoordelingsEngine() {
            beoordelingsgraden.Add("VOV", 6);
            beoordelingsgraden.Add("OV", 9);
            beoordelingsgraden.Add("V", 11);
            beoordelingsgraden.Add("RV", 13);
            beoordelingsgraden.Add("G", 15);
            beoordelingsgraden.Add("ZG", 20);
        }

        private double getMidden(string sleutel) {
            KeyValuePair<string, double> beoordelingsgraad = KVPHelper.GetEntry(beoordelingsgraden, sleutel);
            int max = (int)beoordelingsgraad.Value;

            if (sleutel.Equals("VOV")) { // VOV: 0-6 (midden = 3)
                return max / 2;
            }
            else if (sleutel.Equals("OV")) { // OV: 7-8-9 (midden = 8)
                return max - 1;
            }
            else if (sleutel.Equals("V") || sleutel.Equals("RV") || sleutel.Equals("G")) {
                // V: 10-11 (midden = 10,5)
                // RV: 12-13 (midden = 12,5)
                // G: 14-15 (midden = 14,5)

                return max - 0.5;
            }

            return 18; // ZG: 16-20 (midden = 18)
        }

        public double deelaspect(KeyValuePair<string, double> keuze, int weging) {
            string sleutel = keuze.Key.ToString();
            double midden = getMidden(keuze.Key.ToString());

            return midden * weging;
        }

        public double totaalDeelaspect(Dictionary<string, double> keuzes, List<int> wegingen) {
            double totaal = 0;
            int i = 0;

            foreach (KeyValuePair<string, double> keuze in keuzes) {
                totaal += deelaspect(keuze, wegingen[i]);

                i++;
            }

            return totaal;
        }

        public double totaalWeging(List<int> wegingen) {
            int totaal = 0;

            foreach (int weging in wegingen) {
                totaal += weging;
            }

            return totaal;
        }

        public double totaalScore(Dictionary<string, double> keuzes, List<int> wegingen) {
            double totaal = 0;

            if (totaalWeging(wegingen) == 0) {
                return totaal;
            }

            totaal = totaalDeelaspect(keuzes, wegingen) / totaalWeging(wegingen);

            double afgerond = Math.Round(totaal, 4);

            return afgerond;
        }
    }
}