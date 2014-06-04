using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * KeyValuePair helper om 1 KeyValuePair uit een Dictionary te kunnen halen
 */

namespace BeoordelingProject.Helpers {
    public static class KVPHelper {
        public static KeyValuePair<string, double> GetEntry(this IDictionary<string, double> dictionary, string key) {
            return new KeyValuePair<string, double>(key, dictionary[key]);
        }
    }
}