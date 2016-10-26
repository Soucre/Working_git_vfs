angular.module('app').controller('createEditJobController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', '$window', function ($scope, $http, ngDialog,ngProgressFactory, $window) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.Model = {};
    $scope.IsEdit = false;
    $scope.Init = function () {
        $scope.progressbar.start();

        if (_jobId !== "") {
            $scope.IsEdit = true;
        } else {
            $scope.IsEdit = false;
        }
        
        $http.get('/api/common/job-type').success(function (result) {
            if (result.Success) {
                $scope.JobTypes = result.Data;
            }
        });

        $http.get('/api/common/education-level').success(function (result) {
            if (result.Success) {
                $scope.EducationLevels = result.Data;
            }
        });

        $http.get('/api/common/experience-level').success(function (result) {
            if (result.Success) {
                $scope.ExperienceLevels = result.Data;
            }
        });

        $http.get('/api/common/languages').success(function (result) {
            if (result.Success) {
                $scope.Languages = result.Data;
            }
        });

        $http.get('/api/common/industries').success(function (result) {
            if (result.Success) {
                $scope.Industries = result.Data;
            }
        });

        $http.get('/api/common/gender-required').success(function (result) {
            if (result.Success) {
                $scope.GenderRequireds = result.Data;
            }
        });

        $http.get('/api/common/locations').success(function (result) {
            if (result.Success) {
                $scope.Locations = result.Data;
            }
        });

        getJob();

        $scope.progressbar.complete();
    }

    var getJob = function () {
        if (_jobId !== "") {
        $http.get('/api/jobs/get/' + _jobId)
            .success(function (result) {
                if (result.Success) {
                    $scope.Model = result.Data;
                    if (result.Data.StartDate != null) {
                        $scope.Model.StartDate = new Date(result.Data.StartDate + "Z");
                    }
                    if (result.Data.EndDate != null) {
                        $scope.Model.EndDate = new Date(result.Data.EndDate + "Z");
                    }
                    if (result.Data.JobStartDate != null) {
                        $scope.Model.JobStartDate = new Date(result.Data.JobStartDate + "Z");
                    }
                    $http.get('/api/jobs/education/' + result.Data.Id).success(function (education) {
                        if (education.Success) {
                            $scope.Model.EducationLevel = education.Data[0].EducationLevel;
                            $scope.Model.FieldOfStudy = education.Data[0].FieldOfStudy;
                            $scope.Model.Certification = education.Data[0].Certification;
                            $scope.Model.MinimumGrade = education.Data[0].MinimumGrade;
                            $scope.Model.ExperienceLevel = education.Data[0].ExperienceLevel;
                        }
                    });
                }
                else {
                    $.unblockUI();
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
    }

    $scope.format = 'MM/dd/yyyy';
    $scope.openJobStartDate = function () {
        $scope.isOpenJobStartDate = true;
    };

    $scope.openStartDate = function () {
        $scope.isOpenStartDate = true;
    };

    $scope.openEndDate = function () {
        $scope.isOpenEndDate = true;
    };

    $scope.createJob = function () {
        if ($scope.createNewJob.$valid) {
            $.blockUI({ message: $('.loader') });
            $http.post('/api/jobs/create', $scope.Model)
            .success(function (result) {
                if (result.Success) {
                    $.unblockUI();
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Job has been added successfully."}'
                    });

                    setTimeout(function() {
                        location.href = location.origin + '/job/employer/all';
                        },
                        2000);
                } else {
                    $.unblockUI();
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
    }

    $scope.updateJob = function () {
        if ($scope.createNewJob.$valid) {
            $.blockUI({ message: $('.loader') });
            $scope.Model.JobID = _jobId;
            $http.post('/api/jobs/update', $scope.Model)
            .success(function (result) {
                if (result.Success) {
                    $.unblockUI();
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Job has been updated successfully."}'
                    });

                    setTimeout(function () {
                        location.href = location.origin + '/job/employer/all';
                    },
                        2000);
                } else {
                    $.unblockUI();
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
    }
}]);