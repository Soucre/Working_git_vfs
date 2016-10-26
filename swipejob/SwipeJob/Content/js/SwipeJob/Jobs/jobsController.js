angular.module('app').controller('jobsController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.maxSize = 5;
    $scope.itemPerPage = 5;
    $scope.currentPage = 1;
    $scope.maxSizeEmployer = 5;
    $scope.itemEmployerPerPage = 5;
    $scope.EmployerName = "";
    var searchParam = {};
    var employerSeach = {};
    $scope.Init = function () {
        $scope.progressbar.start();
        $http.get('/api/common/industries').success(function (result) {
            if (result.Success) {
                $scope.Categories = result.Data;
            }
        });
        $http.get('/api/common/locations').success(function (result) {
            if (result.Success) {
                $scope.Locations = result.Data;
            }
        });

        $http.get('/api/common/job-type').success(function (result) {
            if (result.Success) {
                $scope.JobTypes = result.Data;
            }
        });
        var searchData = sessionStorage.getItem("searchModel");
        if (typeof searchData !== 'undefined' && searchData !== null) {
            $scope.Search = JSON.parse(searchData);
            searchParam = {
                JobTitle: $scope.Search.JobTitle,
                Location: $scope.Search.Location,
                Category: $scope.Search.Category,
                PageIndex: 0,
                PageSize: $scope.itemPerPage
            }
            getData(searchParam);
        }
        employerSeach = {
            PageIndex: "0",
            PageSize: $scope.itemEmployerPerPage
        }
        getEmployerData(employerSeach);

        $scope.progressbar.complete();
    }

    $scope.getImage = function (data) {
        if (data !== "") {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-person.jpg";
        }
    }

    $scope.getEmployerLogo = function (data) {
        if (data !== "") {
            return 'data:image/jpeg;base64,' + data;
        } else {
            return "/Content/images/no-logo.png";
        }
    }

    var getData = function (searchParam) {
        $scope.progressbar.start();
        $http.post('/api/jobs/search', searchParam).success(function (result) {
            if (result.Success) {
                $.each(result.Data, function (i, item) {
                    item.StartDate = new Date(item.StartDate + "Z");
                    item.EndDate = new Date(item.EndDate + "Z");
                });
                $scope.TotalItems = result.TotalItems;
                $scope.Jobs = result.Data;

                var resetSearchData = { JobTitle: "", Location: "", Category: "" }
                sessionStorage.setItem("searchModel", JSON.stringify(resetSearchData));

                $scope.progressbar.complete();
            } else {
                $scope.progressbar.complete();
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
                } else if (result.Data === "INVALID_SESSION") {
                    ngDialog.open({
                        template: 'alert-error',
                        data: '{"message":"Your seesion invaild!"}'
                    });
                }
            }
        });
    }

    $scope.search = function (page) {
        searchParam = {
            JobTitle: $scope.Search.JobTitle,
            Location: $scope.Search.Location,
            Category: $scope.Search.Category,
            JobType:$scope.Search.JobType,
            PositionLevel: $scope.Search.PositionLevel,
            MinSalary: $scope.Search.MinSalary,
            MaxSalary: $scope.Search.MaxSalary,
            PageIndex: page,
            PageSize: $scope.itemPerPage
        }
        getData(searchParam);
    }

    $scope.pageChanged = function (page) {
        searchParam = {
            JobTitle: $scope.Search.JobTitle,
            Location: $scope.Search.Location,
            Category: $scope.Search.Category,
            JobType: $scope.Search.JobType,
            PositionLevel: $scope.Search.PositionLevel,
            MinSalary: $scope.Search.MinSalary,
            MaxSalary: $scope.Search.MaxSalary,
            PageIndex: page - 1,
            PageSize: $scope.itemPerPage
        }
        getData(searchParam);
    }

    //employer
    var getEmployerData = function (value) {
        $scope.progressbar.start();
        $http.post('/api/employer/search', value)
      .success(function (result) {
          if (result.Success) {
              $scope.TotalEmployerItems = result.TotalItems;
              $scope.employerItems = result.Data;
              $scope.progressbar.complete();
          } else {
              $scope.progressbar.complete();
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
              } else if (result.Data === "INVALID_SESSION") {
                  ngDialog.open({
                      template: 'alert-error',
                      data: '{"message":"Your seesion invaild!"}'
                  });
              }
          }
      });
    }

    $scope.searchEmployer = function (page) {
        employerSeach = {
            CompanyName: $scope.EmployerName,
            PageIndex: page,
            PageSize: $scope.itemEmployerPerPage
        };
        getEmployerData(employerSeach);
    }

    $scope.pageEmployerChanged = function (page) {
        var employerSeach = {
            CompanyName: $scope.EmployerName,
            PageIndex: page - 1,
            PageSize: $scope.itemEmployerPerPage
        };
        getEmployerData(employerSeach);
    }
    $scope.goToEmployerDashboard = function (id) {
        location.href = location.origin + '/employer/' + id + '/dashboad';
    }
    $scope.goToJobDetail = function (id) {
        location.href = location.origin + '/job/' + id + '/detail';
    }
    $scope.logInToViewSalary = function() {
        location.href = location.origin + '/job-seeker/login?ReturnUrl=' + location.pathname;
    }

    $scope.searchAllEmployerJob = function (employerName) {
        $scope.Search.JobTitle = employerName;
       var searchParam = {
            JobTitle: $scope.Search.JobTitle,
            Location: "",
            Category: "",
            PositionLevel: "",
            MinSalary: "",
            MaxSalary: "",
            PageIndex: 0,
            PageSize: $scope.itemPerPage
        }
        getData(searchParam);
    }
}]);