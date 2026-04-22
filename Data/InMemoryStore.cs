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
    }
}
