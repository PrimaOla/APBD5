using APBD5.Models;

namespace APBD5.Data
{
    public static class InMemoryStore
    {
        public static readonly List<Room> Rooms = new()
        {
            new Room { Id = 1, Name = "A1",      BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true,  IsActive = true  },
            new Room { Id = 2, Name = "B1",      BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true  },
            new Room { Id = 3, Name = "C1",      BuildingCode = "A", Floor = 3, Capacity = 15, HasProjector = true,  IsActive = false },
            new Room { Id = 4, Name = "Lab 101", BuildingCode = "C", Floor = 1, Capacity = 24, HasProjector = true,  IsActive = true  },
            new Room { Id = 5, Name = "Aula",    BuildingCode = "B", Floor = 0, Capacity = 80, HasProjector = true,  IsActive = true  }
        };

        public static readonly List<Reservation> Reservations = new()
        {
            new Reservation
            {
                Id = 1, RoomId = 1,
                OrganizerName = "Anna Kowalska", Topic = "Warsztaty z HTTP i REST",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 30),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 2, RoomId = 1,
                OrganizerName = "Piotr Nowak", Topic = "Konsultacje projektowe",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(14, 0),
                Status = "planned"
            },
            new Reservation
            {
                Id = 3, RoomId = 2,
                OrganizerName = "Marta Zielinska", Topic = "Szkolenie ASP.NET",
                Date = new DateOnly(2026, 5, 11),
                StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 4, RoomId = 4,
                OrganizerName = "Jakub Wisniewski", Topic = "Laboratorium sieciowe",
                Date = new DateOnly(2026, 5, 12),
                StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0),
                Status = "planned"
            },
            new Reservation
            {
                Id = 5, RoomId = 5,
                OrganizerName = "Ewa Lewandowska", Topic = "Wyklad otwarty",
                Date = new DateOnly(2026, 5, 15),
                StartTime = new TimeOnly(18, 0), EndTime = new TimeOnly(20, 0),
                Status = "cancelled"
            }
        };
    }
}
