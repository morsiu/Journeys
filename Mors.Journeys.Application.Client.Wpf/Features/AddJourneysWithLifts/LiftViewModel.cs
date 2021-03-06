﻿using System.ComponentModel;
using Mors.Journeys.Application.Client.Wpf.Infrastructure.Extensions;
using Mors.Journeys.Data.Commands.Dtos;

namespace Mors.Journeys.Application.Client.Wpf.Features.AddJourneysWithLifts
{
    internal sealed class LiftViewModel : INotifyPropertyChanged
    {
        private string _passengerName;
        private decimal _liftDistance;

        public string PassengerName
        {
            get { return _passengerName; }
            set 
            { 
                _passengerName = value;
                PropertyChanged.Raise(this);
            }
        }

        public decimal LiftDistance
        {
            get { return _liftDistance; }
            set 
            { 
                _liftDistance = value;
                PropertyChanged.Raise(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Lift ToDto()
        {
            return new Lift(PassengerName, LiftDistance);
        }
    }
}
