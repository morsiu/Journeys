var PassengerService = ['$http', function ($http) {
    this.getNames = function () {
        return $http({
            method: 'POST',
            url: '../api/query',
            data: "{ $type: 'Journeys.Queries.GetPeopleNamesQuery, Journeys.Queries' }"
        });
    };
}];