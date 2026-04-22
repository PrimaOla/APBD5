# APBD5

ASP.NET Core Web API do zarzadzania salami i rezerwacjami w centrum szkoleniowym. Dane sa przechowywane w pamieci aplikacji w statycznych listach, bez bazy danych i bez Entity Framework Core.

## Zakres projektu

- zarzadzanie salami (`RoomsController`)
- zarzadzanie rezerwacjami (`ReservationsController`)
- filtrowanie po parametrach z query string
- walidacja modeli przez `Data Annotations`
- zwracanie poprawnych statusow HTTP
- proste reguly biznesowe dla rezerwacji

## Technologie

- .NET 9
- ASP.NET Core Web API
- kontrolery z `[ApiController]`
- dane seedowane w `Data/InMemoryStore.cs`

## Struktura projektu

- `Program.cs` - konfiguracja aplikacji
- `Controllers/RoomsController.cs` - endpointy sal
- `Controllers/ReservationsController.cs` - endpointy rezerwacji
- `Models/Room.cs` - model sali
- `Models/Reservation.cs` - model rezerwacji i walidacja czasu
- `Data/InMemoryStore.cs` - statyczne listy z danymi startowymi

## Uruchomienie

1. Wejdz do katalogu projektu.
2. Uruchom aplikacje:

```bash
dotnet run
```

3. Sprawdz w konsoli, pod jakim adresem wystartowala aplikacja.

Przykladowe adresy:

- `http://localhost:5000`
- `https://localhost:5001`

Jesli u Ciebie port jest inny, uzyj adresu wyswietlonego przez `dotnet run`.

## Dane startowe

Aplikacja startuje z:

- 5 salami
- 5 rezerwacjami

Dane sa definiowane w `Data/InMemoryStore.cs`.

## Endpointy

### Rooms

- `GET /api/rooms` - wszystkie sale
- `GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true` - filtrowanie sal
- `GET /api/rooms/{id}` - jedna sala po id
- `GET /api/rooms/building/{buildingCode}` - sale z podanego budynku
- `POST /api/rooms` - dodanie nowej sali
- `PUT /api/rooms/{id}` - pelna aktualizacja sali
- `DELETE /api/rooms/{id}` - usuniecie sali

### Reservations

- `GET /api/reservations` - wszystkie rezerwacje
- `GET /api/reservations?date=2026-05-10&status=confirmed&roomId=1` - filtrowanie rezerwacji
- `GET /api/reservations/{id}` - jedna rezerwacja po id
- `POST /api/reservations` - dodanie nowej rezerwacji
- `PUT /api/reservations/{id}` - aktualizacja rezerwacji
- `DELETE /api/reservations/{id}` - usuniecie rezerwacji

## Walidacja

### Room

- `Name` - wymagane, max 100 znakow
- `BuildingCode` - wymagane, max 10 znakow
- `Floor` - zakres `-5..100`
- `Capacity` - minimum `1`

### Reservation

- `RoomId` - minimum `1`
- `OrganizerName` - wymagane, max 100 znakow
- `Topic` - wymagane, max 200 znakow
- `Status` - tylko `planned`, `confirmed`, `cancelled`
- `EndTime` musi byc pozniejsze niz `StartTime`

## Reguly biznesowe

- nie mozna dodac rezerwacji do nieistniejacej sali
- nie mozna dodac rezerwacji do sali nieaktywnej
- dwie rezerwacje tej samej sali nie moga nakladac sie czasowo tego samego dnia
- nie mozna usunac sali, jesli ma przypisane rezerwacje

## Statusy HTTP

- `200 OK` - poprawny odczyt lub aktualizacja
- `201 Created` - poprawne utworzenie zasobu
- `204 No Content` - poprawne usuniecie
- `400 Bad Request` - bledne dane wejsciowe
- `404 Not Found` - zasob nie istnieje
- `409 Conflict` - konflikt biznesowy

## Przykladowe requesty

### POST /api/rooms

```json
{
  "name": "Lab 204",
  "buildingCode": "B",
  "floor": 2,
  "capacity": 24,
  "hasProjector": true,
  "isActive": true
}
```

### POST /api/reservations

```json
{
  "roomId": 1,
  "organizerName": "Anna Kowalska",
  "topic": "Warsztaty z HTTP i REST",
  "date": "2026-05-10",
  "startTime": "15:00:00",
  "endTime": "16:30:00",
  "status": "confirmed"
}
```

## Przykladowe testy w Postmanie

- pobranie wszystkich sal
- filtrowanie sal po `minCapacity`, `hasProjector`, `activeOnly`
- pobranie sal z konkretnego budynku
- dodanie nowej sali
- aktualizacja sali
- usuniecie sali bez rezerwacji
- proba usuniecia sali z rezerwacjami
- pobranie wszystkich rezerwacji
- filtrowanie rezerwacji po `date`, `status`, `roomId`
- dodanie poprawnej rezerwacji
- proba dodania kolidujacej rezerwacji
- proba dodania rezerwacji do nieaktywnej sali
- usuniecie rezerwacji

## Uwagi

- projekt nie korzysta z bazy danych
- dane znikaja po restarcie aplikacji
- API testowane recznie, np. w Postmanie
