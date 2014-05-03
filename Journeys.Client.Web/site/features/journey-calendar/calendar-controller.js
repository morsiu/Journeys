var CalendarController = ['$scope', function ($scope) {
    $scope.month = 3;
    $scope.year = 2014;

    this.nextMonth = function () {
        $scope.month += 1;
        if ($scope.month > 11) {
            $scope.month = 0;
            $scope.year += 1;
        };
    };

    this.previousMonth = function () {
        $scope.month -= 1;
        if ($scope.month < 0) {
            $scope.month = 11;
            $scope.year -= 1;
        };
    };
}];