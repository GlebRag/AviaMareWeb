using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMareWinForms
{
    internal class Database
    {
        static public List<Plane> planes = new List<Plane>
        {
            new Plane(1, "Boeing 747", 1998, 920, 416),
            new Plane(2, "Airbus A320", 2000, 840, 150),
            new Plane(3, "Concorde", 1976, 2179, 92),
            new Plane(4, "Cessna 172", 1956, 226, 4),
            new Plane(5, "Embraer E190", 2004, 829, 114),
            new Plane(6, "Bombardier CRJ200", 1992, 780, 50),
            new Plane(7, "Antonov An-225", 1988, 850, 250),
            new Plane(8, "Lockheed SR-71", 1966, 3529, 2),
            new Plane(9, "Sukhoi Superjet 100", 2008, 860, 108),
            new Plane(10, "Tupolev Tu-144", 1968, 2430, 140),
            new Plane(11, "Douglas DC-3", 1935, 333, 21),
            new Plane(12, "Fokker F28", 1967, 795, 65),
            new Plane(13, "McDonnell Douglas MD-80", 1979, 811, 172),
            new Plane(14, "Dassault Falcon 7X", 2005, 953, 19),
            new Plane(15, "Boeing 787 Dreamliner", 2009, 913, 296),
            new Plane(16, "Pilatus PC-12", 1991, 500, 9),
            new Plane(17, "Beechcraft King Air 350", 1988, 578, 11),
            new Plane(18, "Airbus A380", 2005, 1020, 853),
            new Plane(19, "Mitsubishi Regional Jet", 2015, 890, 92),
            new Plane(20, "Harrier Jump Jet", 1967, 1185, 1)
        };

        static public List<Ticket> tickets = new List<Ticket>
        {
            new Ticket(1, "Paris", "New York", 5830, planes[0], 7, 1200, new DateTime(2024, 11, 30, 10, 0, 0), new DateTime(2024, 11, 30, 17, 0, 0)),
            new Ticket(2, "London", "Tokyo", 9560, planes[1], 12, 1500, new DateTime(2024, 12, 1, 8, 0, 0), new DateTime(2024, 12, 1, 20, 0, 0)),
            new Ticket(3, "Sydney", "Los Angeles", 12055, planes[2], 14, 2000, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 6, 4, 0, 0)),
            new Ticket(4, "Moscow", "Dubai", 3700, planes[3], 5, 600, new DateTime(2024, 12, 4, 16, 0, 0), new DateTime(2024, 12, 4, 21, 0, 0)),
            new Ticket(5, "Berlin", "Rome", 1180, planes[4], 2, 300, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(6, "Madrid", "Lisbon", 625, planes[5], 1, 150, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 17, 0, 0)),
            new Ticket(7, "Toronto", "Vancouver", 3360, planes[6], 5, 500, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 21, 0, 0)),
            new Ticket(8, "Beijing", "Shanghai", 1080, planes[7], 2, 250, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(9, "Cape Town", "Johannesburg", 1270, planes[8], 2, 200, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(10, "Rio de Janeiro", "Buenos Aires", 1960, planes[9], 3, 400, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 19, 0, 0)),
            new Ticket(11, "Singapore", "Bangkok", 1430, planes[10], 2, 300, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(12, "Seoul", "Hong Kong", 2070, planes[11], 3, 400, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 19, 0, 0)),
            new Ticket(13, "Delhi", "Kolkata", 1310, planes[12], 2, 250, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(14, "New York", "Chicago", 1150, planes[13], 2, 250, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(15, "Miami", "Havana", 370, planes[14], 1, 150, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 17, 0, 0)),
            new Ticket(16, "San Francisco", "Seattle", 1300, planes[15], 2, 200, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(17, "Melbourne", "Auckland", 2620, planes[16], 4, 500, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 20, 0, 0)),
            new Ticket(18, "Dubai", "Istanbul", 3040, planes[17], 5, 700, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 21, 0, 0)),
            new Ticket(19, "Cairo", "Athens", 1120, planes[18], 2, 250, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0)),
            new Ticket(20, "Tokyo", "Seoul", 1200, planes[19], 2, 300, new DateTime(2024, 12, 5, 16, 0, 0), new DateTime(2024, 12, 5, 18, 0, 0))
        };
    }
}
