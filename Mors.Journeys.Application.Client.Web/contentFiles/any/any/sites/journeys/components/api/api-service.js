var ApiService = ['$http', '$window', function ($http, $window) {
    this.query = function (queryType) {
        return $http({
            method: 'POST',
            url:  $window.location.origin + '/api/query',
            data: "{ $type: '" + queryType + ", Mors.Journeys.Data' }"
        });
    };
}];