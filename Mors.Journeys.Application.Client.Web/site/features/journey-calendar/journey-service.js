var JourneyService = ['$http', '$q', function ($http, $q) {
    this.getAll = function () {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: '../api/query',
            data: "{ $type: 'Journeys.Data.Queries.GetJourneysByPassengerThenMonthThenDayQuery, Journeys.Data' }"
        }).success(function (response) {
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