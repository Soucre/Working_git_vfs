
angular.module('app').directive('compile', [
    '$compile', function ($compile) {
        return function (scope, element, attrs) {
            scope.$watch(
                function (scope) {
                    return scope.$eval(attrs.compile);
                },
                function (value) {
                    element.html(value);
                    $compile(element.contents())(scope);
                }
            );
        };
    }
]);
angular.module('app').directive('ckeditor', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
            var config, editor, updateModel;
            config = {
                // CKEditor config goes here
            }
            editor = CKEDITOR.replace(element[0], config);
            if (!ngModel) {
                return;
            }
            editor.on('instanceReady', function () {
                return editor.setData(ngModel.$viewValue);
            });
            updateModel = function () {
                return scope.$apply(function () {
                    return ngModel.$setViewValue(editor.getData());
                });
            };
            editor.on('change', updateModel);
            editor.on('dataReady', updateModel);
            editor.on('key', updateModel);
            editor.on('paste', updateModel);
            editor.on('selectionChange', updateModel);
            return ngModel.$render = function () {
                return editor.setData(ngModel.$viewValue);
            };
        }
    };
});

angular.module('app').directive("passwordVerify", function () {
    return {
        require: "ngModel",
        scope: {
            passwordVerify: '='
        },
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(function () {
                var combined;

                if (scope.passwordVerify || ctrl.$viewValue) {
                    combined = scope.passwordVerify + '_' + ctrl.$viewValue;
                }
                return combined;
            }, function (value) {
                if (value) {
                    ctrl.$parsers.unshift(function (viewValue) {
                        var origin = scope.passwordVerify;
                        if (origin !== viewValue) {
                            ctrl.$setValidity("passwordVerify", false);
                            return undefined;
                        } else {
                            ctrl.$setValidity("passwordVerify", true);
                            return viewValue;
                        }
                    });
                }
            });
        }
    };
});
angular.module('app').filter("trust", ['$sce', function ($sce) {
    return function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    }
}]);