angular.module('app').controller('forgotPasswordController', ['$scope', '$http', 'ngDialog', '$window', function ($scope, $http, ngDialog, $window) {

    $scope.submit = function () {
        if ($scope.forgotPassword.$valid) {
            $.blockUI({ message: $('.loader') });
            $http.post('/api/account/pwd/forgot', $scope.Model)
                .success(function (result) {
                    if (result.Success) {
                        $.unblockUI();
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"We have just send to your email a link to reset password."}'
                        });
                        setTimeout(function () {
                            $window.location.href = location.origin +"/";
                        }, 5000);
                    } else {
                        $.unblockUI();
                        if (result.Data === "EMAIL_INVALID") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"This email is not exsited."}'
                            });
                        } else if (result.Data === "NO_PERMISSION") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Your permission is not enough to perform the action!"}'
                            });
                        } else if (result.Data.indexOf("SYSTEM_ERROR=>") === 0) {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Server is error. Please contact to administrator for support!"}'
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