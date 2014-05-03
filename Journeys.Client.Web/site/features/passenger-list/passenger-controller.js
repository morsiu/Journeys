var PassengerController = ['$scope', 'passenger', function ($scope, service) {
    service.getNames().success(function (response) {
        $scope.passengers = response;
    });
}];