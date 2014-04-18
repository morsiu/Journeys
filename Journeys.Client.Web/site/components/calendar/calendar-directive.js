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
            $scope.dayNames = calendarService.shortDayNames;
            $scope.monthNames = calendarService.longMonthNames;
            $scope.columns = [0, 1, 2, 3, 4, 5, 6];
            $scope.rows = [0, 1, 2, 3, 4, 5, 6]
        }]
    };
};