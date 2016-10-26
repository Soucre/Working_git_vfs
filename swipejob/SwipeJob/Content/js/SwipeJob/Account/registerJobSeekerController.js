angular.module('app').controller('registerJobSeekerController', ['$scope', '$http', 'ngDialog', '$window', 'Facebook', 'GooglePlus', 'ngProgressFactory', function ($scope, $http, ngDialog, $window, Facebook, GooglePlus, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.Model = {};

    $scope.format = 'MM/dd/yyyy';
    $scope.open = function () {
        $scope.isOpen = true;
    };

    $scope.Init = function () {
        $scope.progressbar.start();
        $http.get('/api/common/nric-type').success(function (result) {
            if (result.Success) {
                $scope.NricTypes = result.Data;
                $scope.Model.NRICType = "Citizen";
            }
        });
        $scope.progressbar.complete();
    }

    var submit = function () {
        $.blockUI({ message: $('.loader') });
        $http.post('/api/account/jobseeker/register', $scope.Model)
            .success(function (result) {
                if (result.Success) {
                    $.unblockUI();
                    if ($scope.Model.AccountType === "Email") {
                        ngDialog.open({
                            template: 'alert-ok',
                            data:
                                '{"message":"Thank you for registering with SwipeJob. Please use the link we have just sent to your email to activate your account."}'
                        });
                        setTimeout(function() {
                                $window.location.href = '/';
                            },
                            5000);
                    } else {
                        $window.location.href = '/';
                    }
                } else {
                    $.unblockUI();
                    if ($scope.Model.AccountType === "Google" || $scope.Model.AccountType === "Facebook") {
                        $scope.Model = { NRICType: "Citizen"};
                    }

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
    };

    $scope.signUpWithGoogle = function() {
        GooglePlus.login().then(function (authResult) {
            var token = authResult.access_token;
            GooglePlus.getUser().then(function (user) {
                $scope.Model = {
                    AvatarPath: user.picture,
                    AccountType: "Google",
                    NricType: 1,
                    Email: user.email,
                    FullName: user.name,
                    Token: token
                }
                submit();
            });
        }, function (err) {
            ngDialog.open({
                template: 'alert-error',
                data: '{"message":' + err + '}'
            });
        });
    }

    $scope.signUpWithFacebook = function () {
        Facebook.login(function (response) {
            if (response.status === 'connected') {
                var token = response.authResponse.accessToken;
                Facebook.api('/me?fields=name,email,picture', function (user) {
                    var avatarPath = 'https://graph.facebook.com/' + user.id + '/picture?width=500&height=500';
                    $scope.Model = {
                        AvatarPath: avatarPath,
                        AccountType: "Facebook",
                        NricType: 1,
                        Email: user.email,
                        FullName: user.name,
                        Token: token
                    }
                    submit();
                });
            }
        }, { scope: 'email' });
    };

    $scope.signUp = function () {
        if ($scope.registerJobseeker.$valid) {
            if ($scope.Model.Token !== $scope.Model.ConfirmToken) {
                ngDialog.open({
                    template: 'alert-error',
                    data: '{"message":"Password confirmation does not match."}'
                });
            } else {
                $scope.Model.AccountType = "Email";
                submit();
            }
        }
    }

    $scope.signIn = function () {
        location.href = location.origin + "/job-seeker/login";
    }
}]);

