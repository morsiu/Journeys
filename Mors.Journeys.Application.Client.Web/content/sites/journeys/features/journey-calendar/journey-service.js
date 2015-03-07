var JourneyService = ['api', '$q', function (api, $q) {
    this.getAll = function () {
        var result = $q.defer();
        api.query('Mors.Journeys.Data.Queries.GetJourneysByPassengerThenMonthThenDayQuery')
        .success(function (response) {
            var facts = {};
            response.forEach(function (fact) {
                var passengerId = fact.Key.Passenger.Id;
                var month = fact.Key.Month.MonthOfYear - 1;
                var year = fact.Key.Month.Year;
                var day = fact.Key.Day.DayOfMonth;
                if (!facts[passengerId]) {
                    facts[passengerId] = {};
                };
                if (!facts[passengerId][year]) {
                    facts[passengerId][year] = {};
                }
                if (!facts[passengerId][year][month]) {
                    facts[passengerId][year][month] = {};
                }
                facts[passengerId][year][month][day] = fact.Value;
            });
            result.resolve(facts);
        }).error(function (error) {
            result.reject();
        });
        return result.promise;
    };
}];