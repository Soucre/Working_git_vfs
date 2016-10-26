var swipeJob = angular.module('app', ['ngDialog', 'ui.bootstrap', 'facebook', 'googleplus', 'ngProgress']);

swipeJob.config(['$httpProvider', 'FacebookProvider', 'GooglePlusProvider', function ($httpProvider, FacebookProvider, GooglePlusProvider) {
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    FacebookProvider.init('893823504046469');
    GooglePlusProvider.init({
        clientId: '450483257978-3v86rchcu3dgrg3m58th0bkohdolu554.apps.googleusercontent.com',
        apiKey: 'AIzaSyAEjRFbsEozXxsTlnx2MIRfi-RfKV8BL8s'
    });
    GooglePlusProvider.setScopes('profile email');
}]);
