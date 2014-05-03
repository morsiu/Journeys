var PassengerListDirective = function () {
    return {
        templateUrl: 'features/passenger-list/passenger-list-template.html',
        controller: ['$scope', '$rootScope', 'passenger', function ($scope, $rootScope, service) {
            service.getNames().success(function (response) {
                $scope.passengers = response;
            });

            this.selectPassenger = function (passenger) {
                $rootScope.$broadcast('passengerSelected', passenger);
            };

            $scope.list = this;
        }]
    };
};