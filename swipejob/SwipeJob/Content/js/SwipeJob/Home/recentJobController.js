angular.module('app').controller('recentJobController',  ['$scope', '$http', 'ngDialog', function ($scope, $http, ngDialog) {
    $scope.isHomePage = true;
    $scope.maxSize = 5;
    $scope.itemPerPage = 5;
    $scope.TotalItems = 30;
    $scope.currentPage = 1;
    $scope.jobItems = [
        { CompanyLogoPath: "/Content/images/employer1.png", Link: "/job/a5cd1554-d66b-4a02-890c-a4b8338a19d8/detail", JobName: "Job Title", CompanyName: "Company Name", JobLocation: "Job Location", MinSalary: 99, MaxSalary: 999, JobType: "FULLTIME", DateFrom: "01/01/1991", DateTo: "01/01/1999" },
        { CompanyLogoPath: "/Content/images/employer2.png", Link: "/job/a5cd1554-d66b-4a02-890c-a4b8338a19d8/detail", JobName: "Job Title", CompanyName: "Company Name", JobLocation: "Job Location", MinSalary: 99, MaxSalary: 999, JobType: "FULLTIME", DateFrom: "01/01/1991", DateTo: "01/01/1999" },
        { CompanyLogoPath: "/Content/images/employer3.png", Link: "/job/a5cd1554-d66b-4a02-890c-a4b8338a19d8/detail", JobName: "Job Title", CompanyName: "Company Name", JobLocation: "Job Location", MinSalary: 99, MaxSalary: 999, JobType: "FULLTIME", DateFrom: "01/01/1991", DateTo: "01/01/1999" },
        { CompanyLogoPath: "/Content/images/employer4.png", Link: "/job/a5cd1554-d66b-4a02-890c-a4b8338a19d8/detail", JobName: "Job Title", CompanyName: "Company Name", JobLocation: "Job Location", MinSalary: 99, MaxSalary: 999, JobType: "FULLTIME", DateFrom: "01/01/1991", DateTo: "01/01/1999" },
        { CompanyLogoPath: "/Content/images/employer5.png", Link: "/job/a5cd1554-d66b-4a02-890c-a4b8338a19d8/detail", JobName: "Job Title", CompanyName: "Company Name", JobLocation: "Job Location", MinSalary: 99, MaxSalary: 999, JobType: "FULLTIME", DateFrom: "01/01/1991", DateTo: "01/01/1999" },
        { CompanyLogoPath: "/Content/images/employer6.png", Link: "/job/a5cd1554-d66b-4a02-890c-a4b8338a19d8/detail", JobName: "Job Title", CompanyName: "Company Name", JobLocation: "Job Location", MinSalary: 99, MaxSalary: 999, JobType: "FULLTIME", DateFrom: "01/01/1991", DateTo: "01/01/1999" },
    ];
    $scope.pageChanged = function (page) {

    }
}]);