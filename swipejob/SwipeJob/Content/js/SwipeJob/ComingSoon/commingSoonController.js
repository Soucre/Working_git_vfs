angular.module('app').controller('commingSoonController', ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {
    $scope.Init = function () {
        $.blockUI({ message: $('.loader') });
        $http.get('/api/common/experience-level').success(function (result) {
            if (result.Success) {
                $scope.ExperienceLevels = result.Data;
            }
        });
        $http.get('/api/common/industries').success(function (result) {
            if (result.Success) {
                $scope.Industries = result.Data;
            }
        });
        $.unblockUI();
    }

    $scope.format = 'MM/dd/yyyy';
    $scope.open = function () {
        $scope.isOpen = true;
    };
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }
    $scope.submit = function () {
        if ($scope.register.$valid) {
            $.blockUI({ message: $('.loader') });
            $http.post('/api/comming-soon/register', $scope.Model)
                .success(function (result) {
                    if (result.Success) {
                        $.unblockUI();
                        location.href = location.origin + "/subscribe/success";
                    } else {
                        $.unblockUI();
                        if (result.Data === "EMAIL_EXISTED") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Your email is already subscribed!"}'
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
                        }
                    }
                });
        }
    }
}]);