var CalendarHeaderDirective = function () {
    return {
        transclude: true,
        replace: true,
        require: '^calendar',
        template: '<div data-ng-transclude></div>'
    };
};