angular.module('journeyCalendar', ['calendar', 'api'])
    .directive('journeyCalendar', JourneyCalendarDirective)
    .service('journey', JourneyService);