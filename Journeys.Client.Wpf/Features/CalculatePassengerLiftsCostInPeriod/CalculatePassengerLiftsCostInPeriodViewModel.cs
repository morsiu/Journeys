using Journeys.Client.Wpf.Infrastructure;
using Journeys.Client.Wpf.Infrastructure.Extensions;
using Journeys.Data.Queries;
using Journeys.Data.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Journeys.Client.Wpf.Features.CalculatePassengerLiftsCostInPeriod
{
    internal class CalculatePassengerLiftsCostInPeriodViewModel : INotifyPropertyChanged
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private decimal _totalCost;
        private List<PersonName> _passengers;

        public CalculatePassengerLiftsCostInPeriodViewModel(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            CalculateCommand = new DelegateCommand(Calculate);
        }

        private void Calculate()
        {
            try
            {
                TotalCost = _queryDispatcher.Dispatch(new GetCostOfPassengerLiftsInPeriodQuery(Passenger, new Period(From, To))).TotalCost;
            }
            catch
            {
                TotalCost = 0m;
            }
        }

        public void Refresh()
        {
            try
            {
                Passengers = _queryDispatcher.Dispatch(new GetPeopleNamesQuery()).ToList();
            }
            catch
            {
                Passengers = new List<PersonName>();
            }
        }

        public object Passenger { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public List<PersonName> Passengers
        {
            get { return _passengers; }
            set
            {
                _passengers = value;
                PropertyChanged.Raise(this);
            }
        }

        public decimal TotalCost
        {
            get { return _totalCost; }
            set 
            {
                _totalCost = value;
                PropertyChanged.Raise(this);
            }
        }

        public ICommand CalculateCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
