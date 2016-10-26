angular.module('app').controller('registerEmployerController', ['$scope', '$http', 'ngDialog', '$window', function ($scope, $http, ngDialog, $window) {

    $scope.Model = {};

    $scope.createAccountEmployer = function () {

        if ($scope.registerEmployer.$valid) {
            if ($("#agreed").is(':checked')) {
                $.blockUI({ message: $('.loader') });
                $http.post('/api/account/employer/register', $scope.Model)
                    .success(function (result) {
                        if (result.Success) {

                            ngDialog.open({
                                template: 'alert-ok',
                                data: '{"message":"Thank you for registering with SwipeJob. Please use the password we have just sent to your email to login."}'
                            });
                            setTimeout(function () {
                                $window.location.href = '/';
                            }, 5000);
                        } else {
                            $.unblockUI();
                            if (result.Data === "EMAIL_IN_USED") {
                                ngDialog.open({
                                    template: 'alert-error',
                                    data: '{"message":"Your email is already registered!"}'
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
            } else {
                ngDialog.open({
                    template: 'alert-error',
                    data: '{"message":"You must agree with the term of use."}'
                });
            }
        }
    }
}]);