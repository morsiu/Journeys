angular.module('calendar', [])
    .service('calendarService', CalendarService)
    .directive('calendar', CalendarDirective)
    .directive('calendarItem', CalendarItemDirective)
    .directive('calendarHeader', CalendarHeaderDirective);