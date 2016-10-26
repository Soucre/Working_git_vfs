angular.module('app').controller('jobSeekerDashboadController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.Init = function () {
        $scope.progressbar.start();
        
        $http.get('/api/job-seeker/' + _profileId + '/profile').success(function (result) {
            if (result.Success) {
                result.Data.JobSeeker.DateOfBirth = new Date(result.Data.JobSeeker.DateOfBirth + "Z");
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
        $http.get('/api/job-seeker/' + _profileId + '/recent-company').success(function (result) {
            if (result.Success) {
                $.each(result.Data, function (i, item) {
                    item.From = new Date(item.From + "Z");
                    item.To = new Date(item.To + "Z");
                });
                $scope.CompanyHistories = result.Data;
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
            return "/Content/images/no-person.jpg";
        }
    }

    $scope.dowloadMoreDocument = function (id) {
        location.href = location.origin + '/document/download/' + id;
    }
}]);