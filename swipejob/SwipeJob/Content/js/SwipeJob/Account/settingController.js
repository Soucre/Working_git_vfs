angular.module('app').controller('settingController', ['$scope', '$http', 'ngDialog', '$window', function ($scope, $http, ngDialog, $window) {

    $scope.submit = function () {
        if ($scope.changePassword.$valid) {
            if ($scope.Model.NewPassword != $scope.Model.ConfirmNewPassword) {
                ngDialog.open({
                    template: 'alert-error',
                    data: '{"message":"Password confirmation does not match."}'
                });
            } else {
                $.blockUI({ message: $('.loader') });
                var data = {
                    UserId: _userId,
                    CurrentPassword: $scope.Model.CurrentPassword,
                    NewPassword: $scope.Model.NewPassword
                };

                $http.post('/api/account/pwd/change', data).
                    success(function (result) {
                        if (result.Success) {
                            $.unblockUI();
                            ngDialog.open({
                                template: 'alert-ok',
                                data: '{"message":"Your password has been changed successfully."}'
                            });
                            $scope.Model = {};
                        } else {
                            $.unblockUI();
                            if (result.Data === "CURRENT_PASSWORD_INCORRECT") {
                                ngDialog.open({
                                    template: 'alert-error',
                                    data: '{"message":"Your current password is invalid."}'
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