angular.module('app').controller('homeEmployerController',  ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {
    $scope.isHomePage = true;
    $scope.employerItems = [
        { CompanyLogoPath: "/Content/images/employer1.png", Link: "/", CompanyName: "Company Name", JobLocation: "Job Location", Phone: "0977998747", Website: "swipejob.com" },
        { CompanyLogoPath: "/Content/images/employer2.png", Link: "/", CompanyName: "Company Name", JobLocation: "Job Location", Phone: "0977998747", Website: "swipejob.com" },
        { CompanyLogoPath: "/Content/images/employer3.png", Link: "/", CompanyName: "Company Name", JobLocation: "Job Location", Phone: "0977998747", Website: "swipejob.com" },
        { CompanyLogoPath: "/Content/images/employer4.png", Link: "/", CompanyName: "Company Name", JobLocation: "Job Location", Phone: "0977998747", Website: "swipejob.com" },
        { CompanyLogoPath: "/Content/images/employer5.png", Link: "/", CompanyName: "Company Name", JobLocation: "Job Location", Phone: "0977998747", Website: "swipejob.com" },
        { CompanyLogoPath: "/Content/images/employer6.png", Link: "/", CompanyName: "Company Name", JobLocation: "Job Location", Phone: "0977998747", Website: "swipejob.com" }
    ];
}]);