angular.module('calendar', [])
    .service('calendarService', CalendarService)
    .directive('calendar', CalendarDirective);