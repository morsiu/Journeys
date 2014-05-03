var PassengerListDirective = function () {
    return {
        templateUrl: 'features/passenger-list/passenger-list-template.html',
        controller: ['$scope', 'passenger', function ($scope, service) {
            service.getNames().success(function (response) {
                $scope.passengers = response;
            });
        }]
    };
};