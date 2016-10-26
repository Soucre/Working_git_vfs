angular.module('app').controller('activeAccountJobSeekerController', ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {

    $.blockUI({ message: $('.loader') });
    $http.get('/api/account/jobseeker/' + _code + '/active')
            .success(function (result) {
                if (result.Success) {
                    $.unblockUI();
                    $scope.Message = "Your account has been sucucessfuly actived.";
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
                        $scope.Message = "The activation code is not valid.";
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
}]);