using System;
namespace BeoordelingProject.Engine {
    public interface IBeoordelingsEngine {
        double deelaspect(double midden, int weging);
        double totaalDeelaspect(System.Collections.Generic.List<double> middens, System.Collections.Generic.List<int> wegingen);
        double totaalScore(System.Collections.Generic.List<double> middens, System.Collections.Generic.List<int> wegingen);
        double totaalWeging(System.Collections.Generic.List<int> wegingen);
    }
}
