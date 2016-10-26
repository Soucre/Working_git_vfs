angular.module('app').controller('homeSearchController', ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {
    $scope.Init = function () {
        $scope.Search = {
            JobTitle: "",
            Location: "",
            Category: ""
        };
        $.blockUI({ message: $('.loader') });
        $http.get('/api/common/industries').success(function (result) {
            if (result.Success) {
                $scope.Categories = result.Data;
            }
        });
        $http.get('/api/common/locations').success(function (result) {
            if (result.Success) {
                $scope.Locations = result.Data;
            }
        });
        $.unblockUI();
    }

    $scope.jobSeekerLogin = function () {
        location.href = location.origin + "/job-seeker/login";
    }

    $scope.employerLogin = function () {
        location.href = location.origin + "/employer/login";
    }

    $scope.search = function () {
        sessionStorage.setItem("searchModel", JSON.stringify($scope.Search));
        location.href = location.origin + "/job/search";
    }
}]);