angular.module('app').controller('employerDashboadController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.Init = function () {
        $scope.progressbar.start();
        $http.get('/api/employer/' + _profileId + '/profile').success(function (result) {
            if (result.Success) {
                $scope.User = result.Data;
            } else {
                $scope.progressbar.complete();
                if (result.Data === "NO_PERMISSION") {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Your permission is not enough to perform the action!"}'
                    });
                } else if (result.Data.indexOf("SYSTEM_ERROR=>") === 0) {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Server is error. Please contact to administrator for support!"}'
                    });
                } else if (result.Data === "INVALID_SESSION") {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Your seesion invaild!"}'
                    });
                }
            }
        });
        $http.get('/api/employer/' + _profileId + '/latest-job').success(function (result) {
            if (result.Success) {
                $.each(result.Data, function (i, item) {
                    item.StartDate = new Date(item.StartDate + "Z");
                    item.EndDate = new Date(item.EndDate + "Z");
                });
                $scope.Jobs = result.Data;
            } else {
                $scope.progressbar.complete();
                if (result.Data === "NO_PERMISSION") {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Your permission is not enough to perform the action!"}'
                    });
                } else if (result.Data.indexOf("SYSTEM_ERROR=>") === 0) {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Server is error. Please contact to administrator for support!"}'
                    });
                } else if (result.Data === "INVALID_SESSION") {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Your seesion invaild!"}'
                    });
                }
            }
        });
        $scope.progressbar.complete();
    }

    $scope.getImage = function (data) {
        if (data !== "") {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-logo.png";
        }
    }
    $scope.goToJobDetail = function (id) {
        location.href = location.origin + '/job/' + id + '/detail';
    }

    $scope.loginToViewSalary = function () {
        location.href = location.origin + '/job-seeker/login?ReturnUrl=' + location.pathname;
    }

    $scope.searchAllEmployerJob = function (employerName) {
        var data = {
            JobTitle: employerName,
            Location: "",
            Category: ""
        }
        sessionStorage.setItem("searchModel", JSON.stringify(data));
        location.href = location.origin + "/job/search";
    }
}]);