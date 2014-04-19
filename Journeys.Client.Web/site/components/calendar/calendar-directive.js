var CalendarDirective = ['calendarService', function (calendarService) {
    return {
        scope: {
            year: '=',
            month: '='
        },
        replace: true,
        transclude: true,
        templateUrl: 'components/calendar/calendar-template.html',
        compile: function(tElement) {
            var columnCount = calendarService.DAYS_IN_WEEK;
            var rowCount = calendarService.MAX_WEEKS_IN_MONTH;
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
                                controller.addDayScope(contentsScope);
                                var contentsLength = contents.length;
                                for (var contentIdx = 0; contentIdx < contentsLength; ++contentIdx) {
                                    var content = contents[contentIdx];
                                    if (content.hasAttribute && content.hasAttribute('data-calendar-item')) {
                                        cell.appendChild(content);
                                    }
                                }
                            });
                        };
                    };
                    var caption = table.getElementsByTagName('caption')[0];
                    transcludeFn(function (contents, contentsScope) {
                        controller.setHeaderScope(contentsScope);
                        var contentsLength = contents.length;
                        for (var contentIdx = 0; contentIdx < contentsLength; ++contentIdx) {
                            var content = contents[contentIdx];
                            if (content.hasAttribute && content.hasAttribute('data-calendar-header')) {
                                caption.appendChild(content);
                            };
                        };
                    });
                }
            };
        },
        controller: ['$scope', function ($scope) {
            var firstWeekDay = calendarService.MONDAY;
            var monthNames = calendarService.longMonthNames;
            var dayScopes = [];
            var headerScope = null;

            var updateDayScope = function (dayScope) {
                dayScope.$calendarYear = $scope.year;
                dayScope.$calendarMonth = $scope.month;
                var date = calendarService.mapCellToDate(
                    dayScope.$row,
                    dayScope.$column,
                    $scope.month,
                    $scope.year,
                    firstWeekDay);
                dayScope.$date = date && date.getDate();
                dayScope.$day = date && date.getDay();
                dayScope.$month = date && date.getMonth();
                dayScope.$year = date && date.getFullYear();
                dayScope.$isInsideCalendarMonth = date && date.getMonth() == $scope.month && date.getFullYear() == $scope.year;
            };

            var updateDayScopes = function () {
                dayScopes.forEach(updateDayScope);
            };

            var updateHeaderScope = function () {
                if (!headerScope) {
                    return;
                }
                headerScope.$monthName = monthNames[$scope.month] || '';
                headerScope.$month = $scope.month;
                headerScope.$year = $scope.year;
            }

            $scope.$watch('month', function () {
                updateDayScopes();
                updateHeaderScope();
            });

            $scope.$watch('year', function () {
                updateDayScopes();
                updateHeaderScope();
            });

            $scope.dayNames = calendarService.getShortDayNames(firstWeekDay);

            this.addDayScope = function (dayScope) {
                dayScopes.push(dayScope);
                updateDayScope(dayScope);
            }

            this.setHeaderScope = function (newHeaderScope) {
                headerScope = newHeaderScope;
                updateHeaderScope();
            }
        }]
    };
}];