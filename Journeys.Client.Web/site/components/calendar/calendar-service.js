var CalendarService = function () {
    this.DAYS_IN_WEEK = 7;
    this.MAX_WEEKS_IN_MONTH = 5;

    this.shortDayNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    this.longMonthNames = [
        'January',
        'February',
        'March',
        'April',
        'May',
        'June',
        'July',
        'August',
        'September',
        'October',
        'November',
        'December'
    ];

    this.SUNDAY = 0
    this.MONDAY = 1;
    this.TUESDAY = 2;
    this.WEDNESDAY = 3;
    this.THURSDAY = 4;
    this.FRIDAY = 5;
    this.SATURDAY = 6;
    this.SUNDAY = 7;

    this.getShortDayNames = function (weekFirstDay) {
        var shortDayNames = new Array(this.DAYS_IN_WEEK);
        for (var idx = 0; idx < this.DAYS_IN_WEEK; ++idx) {
            var shortDayName = this.shortDayNames[idx];
            var day = this.offsetDay(idx, weekFirstDay);
            shortDayNames[day] = shortDayName;
        };
        return shortDayNames;
    };

    this.offsetDay = function (day, weekFirstDay) {
        day = day - weekFirstDay;
        if (day < 0) {
            day += this.DAYS_IN_WEEK;
        }
        return day;
    };

    this.mapCellToDate = function (row, column, month, year, weekFirstDay) {
        var first = new Date(year, month, 1);
        var dayOfFirst = this.offsetDay(first.getDay(), weekFirstDay);
        var dateOfCurrent = (row * this.DAYS_IN_WEEK) + (column + 1) - dayOfFirst;
        var current = new Date(year, month, dateOfCurrent);
        if (current.getFullYear() != year || current.getMonth() != month) {
            return null;
        }
        return current;
    };
};