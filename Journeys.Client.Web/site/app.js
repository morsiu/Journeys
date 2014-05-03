app = angular.module('app', ['calendar', 'test'])
    .controller('calendar', CalendarController)
    .controller('passenger', PassengerController)
    .service('passenger', PassengerService);