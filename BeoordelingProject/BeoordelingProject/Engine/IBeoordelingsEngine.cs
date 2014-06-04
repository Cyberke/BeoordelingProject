using System;
namespace BeoordelingProject.Engine {
    public interface IBeoordelingsEngine {
        double deelaspect(System.Collections.Generic.KeyValuePair<string, double> keuze, int weging);
        System.Collections.Generic.Dictionary<string, double> getBeoordelingsgraden();
        double totaalDeelaspect(System.Collections.Generic.Dictionary<string, double> keuzes, System.Collections.Generic.List<int> wegingen);
        double totaalScore(System.Collections.Generic.Dictionary<string, double> keuzes, System.Collections.Generic.List<int> wegingen);
        double totaalWeging(System.Collections.Generic.List<int> wegingen);
    }
}
