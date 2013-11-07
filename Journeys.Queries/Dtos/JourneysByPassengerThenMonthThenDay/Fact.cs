using System;
using System.Collections.Generic;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    public class Fact
    {
        public Fact(Key key, Value value)
        {
            Key = key;
            Value = value;
        }

        public Key Key { get; private set; }

        public Value Value { get; private set; }
    }
}
