angular.module('app').controller('employerJobsController', ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {    
    $scope.maxSize = 5;
    $scope.itemPerPage = 5;
    $scope.currentPage = 1;
    $scope.reverse = false;

    //get data
    var getData = function (value) {
        $.blockUI({ message: $('.loader') });
        $http.post('/api/jobs/all', value)
            .success(function (result) {
                if (result.Success) {
                    $scope.TotalItems = result.TotalItems;
                    $scope.jobItems = result.Data;
                    $.each(result.Data, function (i, job) {
                        job.CreatedDateUtc = new Date(job.CreatedDateUtc + "Z");
                    });
                    $.unblockUI();
                } else {
                    $.unblockUI();
                    if (result.Data === "INVALID_SESSION") {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"Your session is expired."}'
                        });
                        location.href = location.origin + "/";
                    } else if (result.Data === "NO_PERMISSION") {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"Your permission is not enough to perform the action"}'
                        });
                    } else if (result.Data.indexOf("SYSTEM_ERROR=>") === 0) {
                         ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Server is error. Please contact to administrator for support."}'
                            });
                    } else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"Some input is required, please check your information again!"}'
                        });
                    } else {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"We\'re sorry, an unexpected error occurred. For questions, please contact administrator for support!"}'
                        });
                    }
                }
            });
    }
    var args = {
        PageIndex: "0",
        PageSize: $scope.itemPerPage
    };
    getData(args);

    $scope.pageChanged = function (page) {
        args = {
            PageIndex: page - 1,
            PageSize: $scope.itemPerPage
        };
        getData(args);
    }

    $scope.createNewJob = function () {
        location.href = _jobDetailLink;
    }

    $scope.editJob = function (Id) {
        location.href = _jobDetailLink + "?jobId=" + Id;
    }

    $scope.sortByDate = function () {
        $scope.reverse = !$scope.reverse;
    };
}]);