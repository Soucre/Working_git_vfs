
angular.module('app').controller('loginJobSeekerController', ['$scope', '$http', 'ngDialog', '$window', 'Facebook', 'GooglePlus', function ($scope, $http, ngDialog, $window, Facebook, GooglePlus) {
    $scope.Model = {}
        $scope.returnUrl = _returnUrl;
    var submit = function () {
        $.blockUI({ message: $('.loader') });
        $scope.Model.UserType = "JobSeeker";

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
                    if ($scope.Model.AccountType === "Google" || $scope.Model.AccountType === "Facebook") {
                        $scope.Model = {};
                    }

                    if (result.Data === "USER_NOT_VERIFIED_YET") {
                        ngDialog.open({
                            template: 'alert-error',
                            data: '{"message":"Account is not verified yet. Please use the link in the email registration to active."}'
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
                    } else if (result.Data === "INVALID" || result.Data === "FACEBOOK_INVALID" || result.Data === "GOOGLE_INVALID") {
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
    };

    $scope.loginWithGoogle = function () {
        GooglePlus.login().then(function (authResult) {
            var token = authResult.access_token;
            GooglePlus.getUser().then(function (user) {
                $scope.Model = {
                    AccountType: "Google",
                    IsKeepSignIn: true,
                    Email: user.email,
                    Token: token
                }
                submit();
            });
        }, function (err) {
        });
    }

    $scope.loginWithFacebook = function () {
        Facebook.login(function (response) {
            if (response.status === 'connected') {
                var token = response.authResponse.accessToken;
                Facebook.api('/me?fields=name,email,picture', function (response) {
                    $scope.Model = {
                        AccountType: "Facebook",
                        IsKeepSignIn: true,
                        Email: response.email,
                        Token: token
                    }
                    submit();
                });
            }
        }, { scope: 'email' });
    };

    $scope.login = function () {
        if ($scope.loginJobseeker.$valid) {
            $scope.Model.AccountType = "Email";
            submit();
        }
    }

    $scope.signUp = function () {
        location.href = location.origin + "/job-seeker/register";
    }
}]);