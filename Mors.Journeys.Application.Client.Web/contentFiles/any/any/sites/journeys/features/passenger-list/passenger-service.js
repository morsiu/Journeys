var PassengerService = ['api', function (api) {
    this.getNames = function () {
        return api.query('Mors.Journeys.Data.Queries.GetPeopleNamesQuery');
    };
}];