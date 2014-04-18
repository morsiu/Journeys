var CalendarDirective = function () {
    return {
        scope: {
            year: '=',
            month: '='
        },
        replace: true,
        transclude: true,
        templateUrl: 'components/calendar/calendar-template.html',
        controller: ['$scope', 'calendarService', function ($scope, calendarService) {
            $scope.columns = [0, 1, 2, 3, 4, 5, 6];
            $scope.rows = [0, 1, 2, 3, 4];

            $scope.dayNames = calendarService.shortDayNames;

            var monthNames = calendarService.longMonthNames;
            $scope.$watch('month', function () {
                $scope.monthName = monthNames[$scope.month] || '';
            });
        }]
    };
};