const queryUrl = `${__API_URL__}/api/query`

export async function journeys() {
    const response = await fetch(queryUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "$type": "Mors.Journeys.Data.Queries.GetJourneysByPassengerThenMonthThenDayQuery, Mors.Journeys.Data" })
    });
    const facts = await response.json();
    const journeys =
        facts.reduce((journeys, fact) => {
            const passengerId = fact.Key.Passenger.Id;
            const year = fact.Key.Month.Year;
            const month = fact.Key.Month.MonthOfYear;
            const day = fact.Key.Day.DayOfMonth;
            const values = {
                liftDistance: fact.Value.LiftDistance,
                liftCount: fact.Value.LiftCount,
                journeyDistance: fact.Value.JourneyDistance,
                journeyCount: fact.Value.JourneyCount
            };
            let journeysOfPassenger = journeys[passengerId];
            if (journeysOfPassenger === undefined) {
                journeysOfPassenger = {};
                journeys[passengerId] = journeysOfPassenger;
            }
            let journeysOfYear = journeysOfPassenger[year];
            if (journeysOfYear === undefined) {
                journeysOfYear = {};
                journeysOfPassenger[year] = journeysOfYear;
            }
            let journeysOfMonth = journeysOfYear[month];
            if (journeysOfMonth === undefined) {
                journeysOfMonth = {};
                journeysOfYear[month] = journeysOfMonth;
            }
            journeysOfMonth[day] = values;
            return journeys;
        },
            {});
    return journeys;
}

export async function passengers() {
    const response = await fetch(queryUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "$type": "Mors.Journeys.Data.Queries.GetPeopleNamesQuery, Mors.Journeys.Data" })
    });
    const passengers = await response.json();
    passengers.sort((a, b) => { a.Name.localeCompare(b.Name) })
    return passengers.map(x => ({ name: x.Name, id: x.OwnerId }));
}