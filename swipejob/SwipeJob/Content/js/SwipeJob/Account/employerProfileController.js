angular.module('app').controller('employerProfileController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.isEditCompanyInfo = false;
    $scope.isEditOverView = false;
    $scope.Init = function () {
        getData();
    }

    var getData = function () {
        $http.get('/api/account/get-current-user').success(function (result) {
            if (result.Success) {
                $scope.Model = result.Data;
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

    //AVATAR
    $scope.getImage = function (data) {
        if (data !== "" && typeof data !== 'undefined') {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-logo.png";
        }
    }
    $scope.fileChanged = function (elm) {
        $scope.files = elm.files;
        if (elm.value.length != 0) {
            $scope.progressbar.start();
            $scope.$apply();
            var fd = new FormData();
            angular.forEach($scope.files, function (file) {
                fd.append('file', file);
            });

            $http.post('/api/common/employer/logo-photo', fd,
                {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                .success(function (result) {
                    if (result.Success) {
                        $scope.Model.Employer.LogoImage = result.Data;
                        $scope.progressbar.complete();
                    } else {
                        alert(JSON.stringify(result))
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
                        } else {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"We\'re sorry, an unexpected error occurred. For questions, please contact administrator for support!"}'
                            });
                        }
                    }
                });
        } 
    };

    //Company INFO
    $scope.editCompanyInfo = function () {
        $scope.isEditCompanyInfo = true;
    }

    $scope.saveCompanyInfo = function (conpanyInfo) {
        if (conpanyInfo.$valid) {
            var model = {
                UserId: $scope.Model.Id,
                CompanyName: $scope.Model.Employer.CompanyName,
                CompanyRegistrationNumber: $scope.Model.Employer.CompanyRegistrationNumber,
                Address: $scope.Model.Employer.Address,
                WebLink: $scope.Model.Employer.WebLink,
                ContactName: $scope.Model.Employer.ContactName,
                PhoneNumber: $scope.Model.Employer.PhoneNumber,
                NatureOfBusiness: $scope.Model.Employer.NatureOfBusiness
            }
            $.blockUI({ message: $('.loader') });
            $http.post('/api/account/employer/profile-update/company-info', model)
                .success(function(result) {
                    if (result.Success) {
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"Employer profile updated!"}'
                        });
                        $.unblockUI();
                        getData();
                        $scope.isEditCompanyInfo = false;
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
                        }else if (result.Data.indexOf("ARGUMENT_IS_REQUIRED=>") === 0) {
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

    $scope.cancelCompanyInfo = function () {
        getData();
        $scope.isEditCompanyInfo = false;
    }

    //Overview
    $scope.editOverView = function () {
        $scope.isEditOverView = true;
    }

    $scope.saveOverView = function () {
        var model = {
            UserId: $scope.Model.Id,
            OverView: $scope.Model.Employer.OverView
        }
        $.blockUI({ message: $('.loader') });
        $http.post('/api/account/employer/profile-update/overview', model)
            .success(function (result) {
                if (result.Success) {
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Overview profile updated!"}'
                    });
                    $.unblockUI();
                    $scope.isEditOverView = false;
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

    $scope.cancelOverView = function () {
        getData();
        $scope.isEditOverView = false;
    }

}]);