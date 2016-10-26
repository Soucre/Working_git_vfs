angular.module('app').controller('jobDetailController', ['$scope', '$http', 'ngDialog', 'ngProgressFactory', function ($scope, $http, ngDialog, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.Model = {};
    $scope.ApplicantStatus = 0;
    $scope.Init = function () {
        $scope.progressbar.start();
        $http.get('/api/jobs/get/' + _jobId)
           .success(function (result) {
               if (result.Success) {
                   var job = result.Data;
                   var employer = result.Data.Employer;
                   var location = result.Data.Location;
                   var language = result.Data.Language;

                   $scope.Model = {
                       JobId: _jobId,
                       EmployerId: employer.UserId,
                       CompanyOverview: employer.OverView,
                       JobName: job.JobName,
                       CompanyName: employer.CompanyName,
                       JobLocation: location.Name,
                       EmployerPhone: employer.PhoneNumber,
                       EmployerMail: "admin@swipejob.com",
                       EmployerSite: employer.WebLink,
                       MinSalary: job.MinSalary,
                       MaxSalary: job.MaxSalary,
                       JobType: job.JobTypeText,
                       StartDate: new Date(job.StartDate + "Z"),
                       EndDate: new Date(job.EndDate + "Z"),
                       JobDescription: job.JobDescription,
                       Language: language.Name,
                       StartAt: job.StartWorkingAt,
                       EndAt: job.EndWorkingAt,
                       JobStartDate: new Date(job.JobStartDate + "Z"),
                       HourPerDay: job.HoursPerDay,
                       DayPerWeek: job.DayPerWeek,
                       DayPerMonth: job.DayPerMonth,
                       GenderRequired: job.GenderRequired,
                       ExtraSalary: job.ExtraSalary
                   };

                   if (employer.Logo === null) {
                       $scope.Model.CompanyLogoPath = "/Content/images/no-person.jpg";
                   } else {
                       $scope.Model.CompanyLogoPath = 'data:image/jpeg;base64,' + employer.Logo;
                   }
                   $scope.Model.AbleStartWorkNow = job.IsStartWorkImmediately ? "Yes" : "No";
                   $scope.Model.IsSalaryIncludeMealAndBreakTime = job.IsSalaryIncludeMealAndBreakTime ? "Yes" : "No";

                   $http.get('/api/jobs/education/' + _jobId)
                         .success(function (result) {
                             if (result.Success) {
                                 var fieldOfStudy = result.Data[0].FieldOfStudy;
                                 $scope.Model.FieldOfStudy = fieldOfStudy.Name;
                                 $scope.Model.EducationLevel = result.Data[0].EducationLevelText;
                                 $scope.Model.ExperienceLevel = result.Data[0].ExperienceLevelText;
                                 $scope.Model.Certifications = result.Data[0].Certification;
                                 $scope.Model.MinimumGrade = result.Data[0].MinimumGrade;
                             } else {
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

               } else {
                   alert(JSON.stringify(result));
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
        var applyJobId = sessionStorage.getItem("ApplyJobId");
        if (applyJobId !== null) {
            $scope.ApplyJob(applyJobId);
            sessionStorage.removeItem("ApplyJobId");
        }

        if (_isAuthenticated) {
            $http.get('/api/jobs/job-seeker/check-job-applied/' + _jobId)
                         .success(function (result) {
                             if (result.Success) {
                                 $scope.ApplicantStatus = result.Data;
                             } else {
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
        $scope.progressbar.complete();
    }

    $scope.goToEmployerProfile = function (employerId) {
        location.href = location.origin + '/employer/' + employerId + '/dashboad';
    };

    $scope.ApplyJob = function (jobId) {
        $scope.progressbar.start();
        var data = {
            JobId: jobId
        }
        $http.post('/api/jobs/job-seeker/apply', data)
                .success(function (result) {
                    $scope.progressbar.complete();
                    if (result.Success) {
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"You have just applied this job successfully."}'
                        });
                        $scope.ApplicantStatus = 1;
                    } else {
                        $scope.progressbar.complete();
                        if (result.Data === "JOB_ALREADY_APPLIED") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"You already applied this job."}'
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
                        } else if (result.Data === "INVALID_SESSION") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Your seesion invaild!"}'
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

    $scope.SaveJob = function (jobId) {
        $scope.progressbar.start();
        var data = {
            JobId: jobId
        }
        $http.post('/api/jobs/job-seeker/save', data)
                .success(function (result) {
                    $scope.progressbar.complete();
                    if (result.Success) {
                        ngDialog.open({
                            template: 'alert-ok',
                            data: '{"message":"You have just saved this job successfully."}'
                        });
                        $scope.ApplicantStatus = 2;
                    } else {
                        $scope.progressbar.complete();
                        if (result.Data === "JOB_ALREADY_SAVED") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"You already saved this job."}'
                            });
                        } else if (result.Data === "JOB_ALREADY_APPLIED") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"You already applied this job."}'
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
                        } else if (result.Data === "INVALID_SESSION") {
                            ngDialog.open({
                                template: 'alert-error',
                                data: '{"message":"Your seesion invaild!"}'
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

    $scope.loginToViewSalary = function () {
        location.href = location.origin + '/job-seeker/login?ReturnUrl=' + location.pathname;
    }
    $scope.loginToApply = function (jobId) {
        location.href = location.origin + '/job-seeker/login?ReturnUrl=' + location.pathname;
        sessionStorage.setItem("ApplyJobId", jobId);
    }
}]);