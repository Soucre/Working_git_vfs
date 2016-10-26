angular.module('app').controller('companyLogoController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.Init = function () {
        $scope.progressbar.start();
        $http.get('/api/employer/top-employer').success(function (result) {
            if (result.Success) {
                $scope.Items = result.Data;
            }
        });
        $scope.progressbar.complete();
    }

    $scope.getEmployerLogo = function (data) {
        if (data !== "") {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-logo.png";
        }
    }

    $scope.seachJob = function (value) {
        var data = {
            JobTitle: value
        }
        sessionStorage.setItem("searchModel", JSON.stringify(data));
        location.href = location.origin + "/job/search";
    }
}]);