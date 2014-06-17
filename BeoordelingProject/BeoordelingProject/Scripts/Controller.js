var module = angular.module("appModule", []);

module.controller("appController", function ($scope, $filter, studentenlijst)
{
    var scope = $scope;
    var orderBy = $filter('orderBy');


    scope.head = {
        a: "Student",
        b: "Academiejaar",
        c: "Traject   ",
        d: "Tussentijdse evaluatie",
        e: "Eind evaluatie",
    };

    scope.studenten = studentenlijst.Studenten;

    scope.studentRol = scope.studenten[0].studentRol;

    scope.showOpleiding = function (student, opleiding)
    {
        if(student.opleiding === opleiding)
            return student;
    }

    scope.sort = {
        column: 'a',
        descending: false
    };

    scope.selectedCls = function(column)
    {
        return column == scope.sort.column && 'sort-' + scope.sort.descending;
    }

    scope.changeSorting = function (column) {
        var sort = scope.sort;
        if (sort.column == column) {
            sort.descending = !sort.descending;
        } else {
            sort.column = column;
            sort.descending = false;
        }
        scope.studenten = orderBy(scope.studenten, sort.column, sort.descending);
    };

    scope.toggleStudentRol = function (beoordelaar, beoordelaarID) {
        var formdiv = document.getElementById(beoordelaar);
        var td = document.getElementById(beoordelaarID);

        if (formdiv.style.display == "none") {
            formdiv.style.display = "block";
            formdiv.style.opacity = 1;

            td.className = "collapseMin";
        }
        else {
            formdiv.style.display = "none";
            formdiv.style.opacity = 0;

            td.className = "expanderPlus";
        }
    };
});
