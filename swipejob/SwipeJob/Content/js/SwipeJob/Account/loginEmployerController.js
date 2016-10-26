
angular.module('app').controller('loginEmployerController', ['$scope', '$http', 'ngDialog','$window', function ($scope, $http, ngDialog, $window) {
    $scope.Model = {}
    $scope.returnUrl = _returnUrl;

    $scope.login = function () {
        if ($scope.loginEmployer.$valid) {
            $.blockUI({ message: $('.loader') });

            $scope.Model.AccountType = "Email";
            $scope.Model.UserType = "Employer";

            $http.post('/api/account/login', $scope.Model)
                .success(function (result) {
                    if (result.Success) {
                        $.unblockUI();
                        if ($scope.returnUrl !== "") {
                            $window.location.href = location.origin + $scope.returnUrl;
                        } else {
                            $window.location.href = location.origin;
                        }
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
                        } else if (result.Data === "INVALID") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Login failed. Please try again!"}'
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

    $scope.createNewAccount = function () {
        location.href = location.origin + "/employer/register";
    }

}]);

