angular.module('app').controller('contactUsController', ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {

    $scope.Model = {};

    $scope.sendMessage = function () {
        if ($scope.contactUs.$valid) {
            $.blockUI({ message: $('.loader') });
            $http.post('/api/feedback', $scope.Model)
            .success(function (result) {
                if (result.Success) {
                    $.unblockUI();
                    ngDialog.open({
                        template: 'alert-ok',
                        data: '{"message":"Your information has been send successfully."}'
                    });
                    $scope.Model = {};
                } else {
                    $.unblockUI();
                    if (result.Data.indexOf("SYSTEM_ERROR=>") === 0) {
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