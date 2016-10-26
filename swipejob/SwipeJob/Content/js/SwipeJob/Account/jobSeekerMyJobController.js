angular.module('app').controller('jobSeekerMyJobController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    var searchParam = {};
    $scope.maxSize = 5;
    $scope.itemPerPage = 10;
    $scope.currentPage = 1;
    $scope.Init = function () {
        $scope.progressbar.start();
        $scope.ApplicantStatus = _applicantStatus;
        $http.get('/api/common/applicant-status').success(function (result) {
            if (result.Success) {
                $scope.ApplicantStatuses = result.Data;
            }
        });

        searchParam =
        {
            ApplicantStatus: $scope.ApplicantStatus,
            PageIndex: 0,
            PageSize: $scope.itemPerPage
        }
        getData(searchParam);
        $scope.progressbar.complete();

    }
  
    $scope.getImage = function (data) {
        if (data !== "") {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-person.jpg";
        }
    }

    var getData = function (searchParam) {
        $scope.progressbar.start();
        $http.post('/api/jobs/job-seeker/applicant/search', searchParam).success(function (result) {
            if (result.Success) {
                $.each(result.Data, function (i, item) {
                    item.StartDate = new Date(item.StartDate + "Z");
                    item.EndDate = new Date(item.EndDate + "Z");
                });
                $scope.TotalItems = result.TotalItems;
                $scope.Jobs = result.Data;
                $scope.progressbar.complete();
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
    }

    $scope.search = function (page) {
        searchParam =
       {
           ApplicantStatus: $scope.ApplicantStatus,
           PageIndex: page,
           PageSize: $scope.itemPerPage
       }
        getData(searchParam);
    }

    $scope.pageChanged = function (page) {
        searchParam = {
            ApplicantStatus: $scope.ApplicantStatus,
            PageIndex: page - 1,
            PageSize: $scope.itemPerPage
        }
        getData(searchParam);
    }

    $scope.delete=function(id) {
        $scope.progressbar.start();
        var param = {
            Id: id,
            ApplicantStatus: 'Deleted'
        }
        $http.post('/api/jobs/job-seeker/applicant/update', param).success(function (result) {
            if (result.Success) {
                ngDialog.open({
                    template: 'alert-ok',
                    data: '{"message":"Job deleted!"}'
                });
                getData(searchParam);
                $scope.progressbar.complete();
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
    }

    $scope.apply = function (id) {
        $scope.progressbar.start();
        var param = {
            Id: id,
            ApplicantStatus: 'Applied'
        }
        $http.post('/api/jobs/job-seeker/applicant/update', param).success(function (result) {
            if (result.Success) {
                ngDialog.open({
                    template: 'alert-ok',
                    data: '{"message":"Job is now applied to employer!"}'
                });
                getData(searchParam);
                $scope.progressbar.complete();
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
    }

    $scope.goToJobDetail = function (id) {
        location.href = location.origin + '/job/' + id + '/detail';
    }
}]);