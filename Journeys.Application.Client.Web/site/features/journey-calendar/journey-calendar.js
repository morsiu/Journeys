angular.module('journeyCalendar', ['calendar'])
    .directive('journeyCalendar', JourneyCalendarDirective)
    .service('journey', JourneyService);