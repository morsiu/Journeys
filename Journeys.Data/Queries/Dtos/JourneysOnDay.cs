﻿using System;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos
{
    [DataContract]
    public class JourneysOnDay
    {
        public JourneysOnDay(DateTime day, int journeyCount, decimal totalRouteDistance, decimal totalLiftDistance)
        {
            Day = day;
            JourneyCount = journeyCount;
            TotalRouteDistance = totalRouteDistance;
            TotalLiftDistance = totalLiftDistance;
        }

        [DataMember]
        public DateTime Day { get; private set; }

        [DataMember]
        public int JourneyCount { get; private set; }

        [DataMember]
        public decimal TotalRouteDistance { get; private set; }

        [DataMember]
        public decimal TotalLiftDistance { get; private set; }
    }
}