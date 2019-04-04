angular.module('passengerList', ['api'])
    .service('passenger', PassengerService)
    .directive('passengerList', PassengerListDirective);