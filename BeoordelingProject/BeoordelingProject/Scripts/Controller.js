var module = angular.module("appModule", []);


module.controller("appController", function ($scope, $filter, studentenlijst)
{
    $scope.studenten = studentenlijst;
    var orderBy = $filter('orderBy');


    $scope.showOpleiding = function (student, opleiding)
    {
        if(student.opleiding === opleiding)
            return student;
    }

    $scope.order = function (predicate, reverse) {
        $scope.studenten = orderBy($scope.studenten, predicate, reverse);
    };

    $scope.order('naam', false);


});
