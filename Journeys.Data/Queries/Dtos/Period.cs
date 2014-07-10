using System;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos
{
    [DataContract]
    public sealed class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        [DataMember]
        public DateTime Start { get; private set; }

        [DataMember]
        public DateTime End { get; private set; }
    }
}
