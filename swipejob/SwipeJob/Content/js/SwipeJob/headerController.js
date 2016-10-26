angular.module('app').controller('headerController',  ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {
    $scope.Init=function() {
        $scope.isHomePage = _isHome;
    }
   
    $scope.login = function () {
        ngDialog.openConfirm({
            template: 'login', className: 'ngdialog-theme-default', plain: false, showClose: true, closeByNavigation: false, closeByEscape: true, closeByDocument: true, preCloseCallback: function (value) { },
            controller: [
                '$scope', function ($scope) {
                    $scope.loginJobSeeker = function () {
                        location.href = location.origin + "/job-seeker/login";
                    }

                    $scope.loginEmployer = function () {
                        location.href = location.origin + "/employer/login";
                    }
                }
            ]
        });
    };

    $scope.register = function () {
        ngDialog.openConfirm({
            template: 'register', className: 'ngdialog-theme-default', plain: false, showClose: true, closeByNavigation: false, closeByEscape: true, closeByDocument: true, preCloseCallback: function (value) { },
            controller: [
                '$scope', function ($scope) {
                    $scope.loginJobSeeker = function () {
                        location.href =location.origin + "/job-seeker/register";
                    }

                    $scope.loginEmployer = function () {
                        location.href = location.origin + "/employer/register";
                    }
                }
            ]
        });
    }
}]);