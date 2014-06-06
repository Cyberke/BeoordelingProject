var module = angular.module("studentModule", []);


module.controller("studentController", function ($scope, studentenlijst)
{
    $scope.studenten = studentenlijst.Huisartsen;
});
