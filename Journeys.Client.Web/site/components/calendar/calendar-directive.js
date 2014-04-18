var CalendarDirective = function () {
    return {
        scope: {
            year: '=',
            month: '='
        },
        replace: true,
        transclude: true,
        templateUrl: 'components/calendar/calendar-template.html'
    };
};