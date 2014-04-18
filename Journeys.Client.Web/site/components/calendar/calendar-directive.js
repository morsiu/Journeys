var CalendarDirective = function () {
    return {
        scope: {
            year: '=',
            month: '='
        },
        replace: true,
        transclude: true,
        templateUrl: 'components/calendar/calendar-template.html',
        compile: function(tElement) {
            var columnCount = 7;
            var rowCount = 5;
            var table = tElement[0];
            var tableBody = table.tBodies[0];
            for (var rowIdx = 0; rowIdx < rowCount; ++rowIdx) {
                var row = tableBody.insertRow();
                for (var columnIdx = 0; columnIdx < columnCount; ++columnIdx) {
                    var cell = row.insertCell();
                };
            };
            return {
                pre: function (scope, iElement, iAttrs, controller, transcludeFn) {
                    var table = iElement[0];
                    var tableBody = table.tBodies[0];
                    for (var rowIdx = 0; rowIdx < rowCount; ++rowIdx) {
                        var row = tableBody.rows[rowIdx];
                        for (var columnIdx = 0; columnIdx < columnCount; ++columnIdx) {
                            var cell = row.cells[columnIdx];
                            transcludeFn(function (contents, contentsScope) {
                                contentsScope.$row = rowIdx;
                                contentsScope.$column = columnIdx;
                                var contentsLength = contents.length;
                                for (var contentIdx = 0; contentIdx < contentsLength; ++contentIdx) {
                                    var content = contents[contentIdx];
                                    cell.appendChild(content);
                                }
                            });
                        };
                    };
                }
            };
        },
        controller: ['$scope', 'calendarService', function ($scope, calendarService) {
            $scope.dayNames = calendarService.shortDayNames;

            var monthNames = calendarService.longMonthNames;
            $scope.$watch('month', function () {
                $scope.monthName = monthNames[$scope.month] || '';
            });
        }]
    };
};