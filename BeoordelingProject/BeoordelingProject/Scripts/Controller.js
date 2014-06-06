var module = angular.module("studentModule", []);


module.controller("studentController", function ($scope, studentenlijst)
{
    $scope.studenten = studentenlijst;

    $scope.showOpleiding = function (student, opleiding)
    {
        if(student.opleiding === opleiding)
            return student;
    }
});
