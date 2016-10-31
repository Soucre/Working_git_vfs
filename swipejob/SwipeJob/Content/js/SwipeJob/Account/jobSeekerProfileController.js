angular.module('app').controller('jobSeekerProfileController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', '$timeout', function ($scope, $http, ngDialog, ngProgressFactory, $timeout) {

    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.isEditAdditionalInfo = false;
    $scope.isEditPersonalInfo = false;
    $scope.isEditExperienceLevel = false;
    $scope.isEditInterestIn = false;
    $scope.IsShowCompanyHistory = true;
    $scope.Init = function () {
        $scope.progressbar.start();
        getData();

        $http.get('/api/common/languages').success(function (result) {
            if (result.Success) {
                $scope.Languages = result.Data;
            }
        });

        $http.get('/api/common/experience-years').success(function (result) {
            if (result.Success) {
                $scope.ExperienceYears = result.Data;
            }
        });

        $http.get('/api/common/gender').success(function (result) {
            if (result.Success) {
                $scope.Genders = result.Data;
            }
        });
        $http.get('/api/common/nric-type').success(function (result) {
            if (result.Success) {
                $scope.NricTypes = result.Data;
            }
        });
        $http.get('/api/common/nationnal-service-status').success(function (result) {
            if (result.Success) {
                $scope.NationalServices = result.Data;
            }
        });
        $scope.progressbar.complete();
        var cw = $('.img-thumbnail').width();
        $('.img-thumbnail').css({ 'height': cw + 'px' });
    };

    var getData = function () {
        $http.get('/api/account/get-current-user').success(function (result) {
            if (result.Success) {
                $scope.Model = result.Data;
                $http.get('/api/account/get-company-history').success(function (company) {
                    if (company.Success) {
                        $.each(company.Data, function (i, item) {
                            if (item.From) {
                                item.From = new Date(item.From + "Z");
                            }
                            if (item.To) {
                                item.To = new Date(item.To + "Z");
                            }
                        });
                        $scope.Model.JobSeeker.CompanyHistories = company.Data;
                    }
                });
                if (result.Data.JobSeeker.DateOfBirth != null) {
                    $scope.Model.JobSeeker.DateOfBirth = new Date(result.Data.JobSeeker.DateOfBirth + "Z");
                }
                if ($scope.Model.JobSeeker.CompanyHistories.length === 0) {
                    $scope.IsShowCompanyHistory = false;
                    $scope.Model.JobSeeker.CompanyHistories = [
                        {
                            Id: "",
                            CompanyName: "",
                            Position: "",
                            From: "",
                            To: ""
                        }
                    ];
                }
            } else {
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
    };

    //AVATAR
    $scope.getImage = function (data) {
        if (data !== "" && typeof data !== 'undefined') {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-person.jpg";
        }
    };

    $scope.fileChanged = function (elm) {
        $scope.progressbar.start();
        $scope.files = elm.files;
        $scope.$apply();
        var fd = new FormData();
        if ($scope.files.length >= 0) {
            angular.forEach($scope.files, function (file) {
                fd.append('file', file);
            });
            $http.post('/api/common/job-seeker/profile-photo', fd,
            {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
            .success(function (result) {
                if (result.Success) {
                    $scope.Model.JobSeeker.AvartarImage = result.Data;
                    $scope.progressbar.complete();
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Your avatar is updated!"}'
                    });
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

    };

    $scope.uploadMoreDocument = function (elm) {
        $scope.progressbar.start();
        $scope.files = elm.files;
        $scope.$apply();
        var fd = new FormData();
        if ($scope.files.length >= 0) {
            angular.forEach($scope.files, function (file) {
                fd.append('file', file);
            });
            $http.post('/api/common/job-seeker/upload-more-document', fd,
            {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
            .success(function (result) {
                if (result.Success) {
                    $scope.progressbar.complete();
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Upload completed!"}'
                    });
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

    };

    $scope.dowloadMoreDocument = function (id) {
        location.href = location.origin + '/document/download/' + id;
    }

    //PERSONAL INFO
    $scope.datetimeModel = {}
    $scope.format = 'MM/dd/yyyy';
    $scope.open = function ($event, isOpen) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.datetimeModel[isOpen] = !$scope.datetimeModel[isOpen];
    };

    $scope.isOpenFrom = [];
    $scope.isOpenTo = [];
    $scope.openFrom = function (index) {
        $timeout(function () {
            $scope.isOpenFrom[index] = !$scope.isOpenFrom[index];
        });
    }
    $scope.openTo = function (index) {
        $timeout(function () {
            $scope.isOpenTo[index] = !$scope.isOpenTo[index];
        });
    }

    $scope.editPersonalInfo = function () {
        $scope.isEditPersonalInfo = true;
    }

    $scope.savePersonalInfo = function (personalInfo) {
        if (personalInfo.$valid) {
            var model = {
                UserId: $scope.Model.Id,
                FullName: $scope.Model.JobSeeker.FullName,
                Gender: $scope.Model.JobSeeker.Gender,
                NationalServiceStatus: $scope.Model.JobSeeker.NationalServiceStatus,
                DateOfBirth: $scope.Model.JobSeeker.DateOfBirth,
                NRICNumber: $scope.Model.JobSeeker.NRICNumber,
                NRICType: $scope.Model.JobSeeker.NRICType,
                Race: $scope.Model.JobSeeker.Race,
                Religions: $scope.Model.JobSeeker.Religions,
                PhoneNumber: $scope.Model.JobSeeker.PhoneNumber,
                MobileNumber: $scope.Model.JobSeeker.MobileNumber,
                PostalCode: $scope.Model.JobSeeker.PostalCode,
                Address: $scope.Model.JobSeeker.Address,
                Email: $scope.Model.Email
            }
            $scope.progressbar.start();
            $http.post('/api/account/job-seeker/profile-update/personal-info', model)
                .success(function (result) {
                    getData();
                    if (result.Success) {
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"Profile updated!"}'
                        });
                        $scope.isEditPersonalInfo = false;
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
                        } else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Some input is required, please check your information again!"}'
                            });
                        }
                    }
                });
        }
    };

    $scope.cancelPersonalInfo = function () {
        getData();
        $scope.isEditPersonalInfo = false;
    };
    //EXPERIENCE LEVEL
    $scope.addNewCompanyHistory = function () {
        $scope.Model.JobSeeker.CompanyHistories.push({
            Id: "",
            CompanyName: "",
            Position: "",
            From: "",
            To: ""
        });
    };

    $scope.deleteCompanyHistory = function (index, companyHistory) {
        if (companyHistory.Id === "") {
            $scope.Model.JobSeeker.CompanyHistories.splice(index, 1);
        } else {
            $scope.progressbar.start();
            $http.post('/api/job-seeker/company-history/delete', companyHistory)
                .success(function (result) {
                    if (result.Success) {
                        $scope.Model.JobSeeker.CompanyHistories.splice(index, 1);
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"Your company history is deleted!"}'
                        });
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
                        } else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Some input is required, please check your information again!"}'
                            });
                        }
                    }
                });
        }
    };

    $scope.editExperienceLevel = function () {
        $scope.isEditExperienceLevel = true;
    };

    $scope.saveExperienceLevel = function (experienceLevel) {
        if (experienceLevel.$valid) {
            var model = {
                UserId: $scope.Model.Id,
                ExperienceYear: $scope.Model.JobSeeker.ExperienceYear,
                HighestEducation: $scope.Model.JobSeeker.HighestEducation,
                Language: $scope.Model.JobSeeker.Language,
                CompanyHistories: $scope.Model.JobSeeker.CompanyHistories,
                ExpectedPosition: $scope.Model.JobSeeker.ExpectedPosition,
                ExpectedLocation: $scope.Model.JobSeeker.ExpectedLocation,
                ExpectedJobCategory: $scope.Model.JobSeeker.ExpectedJobCategory,
                ExpectedSalary: $scope.Model.JobSeeker.ExpectedSalary,
                CanNegotiation: $scope.Model.JobSeeker.CanNegotiation
            }
            $scope.progressbar.start();

            $http.post('/api/account/job-seeker/profile-update/experience-level', model)
                .success(function (result) {
                    if (result.Success) {
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"Profile updated!"}'
                        });
                        getData();
                        $scope.progressbar.complete();
                        $scope.isEditExperienceLevel = false;
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
                        } else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Some input is required, please check your information again!"}'
                            });
                        }
                    }
                });
        }
    };

    $scope.cancelExperienceLevel = function () {
        getData();
        $scope.isEditExperienceLevel = false;
    };
    //INTEREST IN
    $scope.editInterestIn = function () {
        $scope.isEditInterestIn = true;
    };

    $scope.saveInterestIn = function () {
        var model = {
            UserId: $scope.Model.Id,
            InterestIn: $scope.Model.JobSeeker.InterestIn
        }
        $scope.progressbar.start();
        $http.post('/api/account/job-seeker/profile-update/interest-in', model)
            .success(function (result) {
                if (result.Success) {
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Profile updated!"}'
                    });
                    getData();
                    $scope.progressbar.complete();
                    $scope.isEditInterestIn = false;
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
                    } else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"Some input is required, please check your information again!"}'
                        });
                    }
                }
            });

    };

    $scope.cancelInterestIn = function () {
        getData();
        $scope.isEditInterestIn = false;
    };
    //ADDITIONAL
    $scope.editAdditionalInfo = function () {
        $scope.isEditAdditionalInfo = true;
    };
    
    $scope.saveAdditionalInfo = function () {
        var model = {
            UserId: $scope.Model.Id,
            AdditionalInfo: $scope.Model.JobSeeker.AdditionalInfo
        }
        $scope.progressbar.start();
        $http.post('/api/account/job-seeker/profile-update/addition-info', model)
            .success(function (result) {
                if (result.Success) {
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Profile updated!"}'
                    });
                    $scope.progressbar.complete();
                    $scope.isEditAdditionalInfo = false;
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
                    } else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"Some input is required, please check your information again!"}'
                        });
                    }
                }
            });
    };

    $scope.cancelAdditionalInfo = function () {
        getData();
        $scope.isEditAdditionalInfo = false;
    }
}]);