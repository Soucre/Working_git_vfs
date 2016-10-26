angular.module('app').controller('resetPasswordController', ['$scope', '$http', 'ngDialog', '$window', function ($scope, $http, ngDialog, $window) {

    $scope.submit = function () {
        if ($scope.resetPassword.$valid) {
            if ($scope.Model.Password != $scope.Model.PasswordConfirmation) {
                ngDialog.open({
                    template: 'alert-error',
                    data: '{"message":"Password confirmation does not match."}'
                });
            } else {
                $.blockUI({ message: $('.loader') });
                var data = {
                    ConfirmationCode: _code,
                    Password: $scope.Model.PasswordConfirmation
                };

                $http.post('/api/account/pwd/reset', data).
                    success(function (result) {
                        if (result.Success) {
                            $.unblockUI();
                            ngDialog.open({
                                template: 'alert-ok',
                                data: '{"message":"Your password has been reset successfully"}'
                            });
                            setTimeout(function () {
                                $window.location.href = '/';
                            }, 5000);
                        } else {
                            $.unblockUI();
                            if (result.Data === "INVALID") {
                                ngDialog.open({
                                    template: 'alert-error',
                                    data: '{"message":"This link is not valid. Please use the reset link is send to your email."}'
                                });
                            }
                            else if (result.Data.indexOf("SYSTEM_ERROR=>") === 0) {
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
            
        }
    }
}]);