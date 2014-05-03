angular.module('passengerList', [])
    .service('passenger', PassengerService)
    .directive('passengerList', PassengerListDirective);