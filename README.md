# Voting Application

A full-stack voting application built with **ASP.NET Core 8** backend and **Angular 17** frontend, featuring real-time updates via SignalR, clean architecture, and one-vote-per-voter enforcement.

## 📋 Features

- ✅ Add candidates and voters
- ✅ Cast votes with real-time UI updates
- ✅ One vote per voter enforcement (database constraint + application logic)
- ✅ Real-time synchronization using SignalR
- ✅ Material Design UI with responsive layout
- ✅ Two tables side-by-side (Voters and Candidates)
- ✅ Vote form with dropdown selection
- ✅ Error handling and validation
- ✅ Clean Architecture backend structure
- ✅ Comprehensive unit tests
- ✅ Swagger API documentation

## 🏗️ Architecture

### Backend (ASP.NET Core 8)

```
src/
├── Voting.Api/              # Presentation layer (Controllers, SignalR Hub)
├── Voting.Application/      # Business logic (Use Cases, DTOs, Validators)
├── Voting.Domain/           # Domain entities (Candidate, Voter, Vote)
└── Voting.Infrastructure/   # Data access (EF Core, DbContext)

tests/
└── Voting.Application.Tests/ # Unit tests for use cases
```

**Key Components:**
- **Domain Entities**: `Candidate`, `Voter`, `Vote`
- **Use Cases**: `CreateCandidate`, `GetAllCandidates`, `CreateVoter`, `GetAllVoters`, `CastVote`
- **API Endpoints**:
  - `GET /api/candidates` - List all candidates
  - `POST /api/candidates` - Create new candidate
  - `GET /api/voters` - List all voters
  - `POST /api/voters` - Create new voter
  - `POST /api/votes` - Cast a vote
- **SignalR Hub**: `/hubs/voting` - Real-time vote updates

### Frontend (Angular 17)

```
frontend/
├── src/app/
│   ├── components/
│   │   ├── candidates-table/     # Candidates display + add dialog
│   │   ├── voters-table/         # Voters display + add dialog
│   │   ├── vote-form/            # Vote casting form
│   │   ├── add-candidate-dialog/  # Add candidate modal
│   │   └── add-voter-dialog/      # Add voter modal
│   ├── models/                    # TypeScript interfaces
│   ├── services/                  # API and real-time services
│   └── app.component.*            # Main dashboard layout
```

**Key Services:**
- `CandidatesService` - Candidate API calls
- `VotersService` - Voter API calls
- `VotesService` - Vote casting API calls
- `VotingRealtimeService` - SignalR connection management

## 🚀 Getting Started

### Prerequisites

- **.NET 8 SDK** ([download](https://dotnet.microsoft.com/download/dotnet/8.0))
- **Node.js 18+** ([download](https://nodejs.org/))
- **Visual Studio 2022** or **Visual Studio Code**

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd "d:\MY PROJECTS\MY\Voting App"
   ```

2. Restore dependencies and build:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Run the API:
   ```bash
   dotnet run --project src/Voting.Api/Voting.Api.csproj
   ```

   The API will be available at `http://localhost:5000`
   - Swagger UI: `http://localhost:5000/swagger`

4. (Optional) Run tests:
   ```bash
   dotnet test tests/Voting.Application.Tests/Voting.Application.Tests.csproj
   ```

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

   The application will be available at `http://localhost:4200`

## 🔄 How It Works

### Voting Flow

1. **Initialize**: Load candidates and voters from the API
2. **Real-time Connection**: Frontend connects to SignalR hub for live updates
3. **Cast Vote**:
   - User selects a voter and a candidate
   - Frontend sends vote request to `POST /api/votes`
   - Backend validates the vote in a transaction:
     - Checks if voter exists
     - Checks if candidate exists
     - Verifies voter hasn't already voted
     - Creates vote record and updates counts
   - Backend broadcasts `VoteCast` event via SignalR
4. **Update UI**: Frontend receives event and updates:
   - Candidate vote count
   - Voter "HasVoted" status
   - Disables form for that voter

### One Vote Per Voter Enforcement

Implemented at two levels:

1. **Database Constraint**: Unique index on `Vote.VoterId` prevents duplicate records
2. **Application Logic**: Transaction checks `Voter.HasVoted` before allowing vote
3. **API Response**: Returns 409 Conflict if voter has already voted

## 📊 Database Schema

Uses **SQLite** (stored in `voting.db` file in the API directory).

**Tables:**
- `Candidates`: Id, Name, VoteCount, CreatedAtUtc
- `Voters`: Id, Name, HasVoted, CreatedAtUtc
- `Votes`: Id, VoterId (UNIQUE), CandidateId, CastAtUtc

## 🧪 Testing

### Backend Tests

Run all tests:
```bash
dotnet test
```

Test file: `tests/Voting.Application.Tests/UseCases/Votes/CastVoteUseCaseTests.cs`

**Coverage:**
- ✅ Valid vote casting
- ✅ Voter already voted prevention
- ✅ Entity not found handling
- ✅ Multiple vote attempts rejection

### Frontend Tests

```bash
cd frontend
npm test
```

## 📝 API Examples

### Create Candidate
```bash
POST /api/candidates
Content-Type: application/json

{
  "name": "Alice Johnson"
}
```

### Create Voter
```bash
POST /api/voters
Content-Type: application/json

{
  "name": "John Doe"
}
```

### Cast Vote
```bash
POST /api/votes
Content-Type: application/json

{
  "voterId": "550e8400-e29b-41d4-a716-446655440000",
  "candidateId": "550e8400-e29b-41d4-a716-446655440001"
}
```

**Response:**
```json
{
  "candidate": {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Alice Johnson",
    "voteCount": 1,
    "createdAtUtc": "2025-12-25T10:00:00Z"
  },
  "voter": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "John Doe",
    "hasVoted": true,
    "createdAtUtc": "2025-12-25T09:00:00Z"
  }
}
```

### Error Response (Already Voted)
```json
{
  "error": "Voter with ID '550e8400-e29b-41d4-a716-446655440000' has already cast their vote."
}
```
Status: `409 Conflict`

## 🔗 File Structure

```
Voting App/
├── VotingApp.sln                          # Solution file
├── src/
│   ├── Voting.Api/
│   │   ├── Controllers/
│   │   │   ├── CandidatesController.cs
│   │   │   ├── VotersController.cs
│   │   │   └── VotesController.cs
│   │   ├── Hubs/
│   │   │   └── VotingHub.cs
│   │   ├── Program.cs
│   │   ├── DatabaseSeeder.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Voting.Api.csproj
│   ├── Voting.Application/
│   │   ├── Dtos/
│   │   ├── UseCases/
│   │   ├── Validators/
│   │   └── Voting.Application.csproj
│   ├── Voting.Domain/
│   │   ├── Candidate.cs
│   │   ├── Voter.cs
│   │   ├── Vote.cs
│   │   └── Voting.Domain.csproj
│   └── Voting.Infrastructure/
│       ├── Persistence/
│       │   └── VotingDbContext.cs
│       └── Voting.Infrastructure.csproj
├── tests/
│   └── Voting.Application.Tests/
│       ├── UseCases/
│       │   └── Votes/
│       │       └── CastVoteUseCaseTests.cs
│       └── Voting.Application.Tests.csproj
└── frontend/
    ├── src/
    │   ├── app/
    │   │   ├── components/
    │   │   ├── models/
    │   │   ├── services/
    │   │   ├── app.component.*
    │   ├── index.html
    │   ├── main.ts
    │   └── styles.css
    ├── angular.json
    ├── package.json
    ├── tsconfig.json
    └── README.md
```

## 🎨 UI/UX Features

- **Responsive Design**: Works on desktop and mobile
- **Material Design**: Professional look using Angular Material
- **Real-time Updates**: Instant UI updates when votes are cast
- **Validation**: Client-side and server-side validation
- **Error Handling**: User-friendly error messages via snackbars
- **Disabled States**: Submit button disabled for voters who've voted
- **Visual Indicators**: Check/X marks for voted status

## 🔐 Data Validation

**Validators Implemented:**
- Candidate name: Required, max 255 characters
- Voter name: Required, max 255 characters
- Vote IDs: Required (non-empty GUIDs)
- One vote per voter: Database constraint + application check

**Error Responses:**
- `400 Bad Request` - Validation errors
- `404 Not Found` - Entity not found
- `409 Conflict` - Voter already voted

## 🚀 Deployment Considerations

### For Production:

1. **Backend**:
   - Use SQL Server instead of SQLite (update connection string in `appsettings.json`)
   - Enable HTTPS in Kestrel
   - Configure CORS for production domain
   - Add authentication/authorization
   - Run migrations: `dotnet ef database update`

2. **Frontend**:
   - Update `API_URL` in services to production endpoint
   - Build for production: `ng build --configuration production`
   - Deploy to CDN or static hosting
   - Configure SignalR URL for production

## 📚 Technologies

**Backend:**
- ASP.NET Core 8
- Entity Framework Core 8
- SQLite 3
- FluentValidation
- SignalR
- Swagger/OpenAPI

**Frontend:**
- Angular 17
- Angular Material 17
- RxJS 7
- TypeScript 5
- SignalR Client

## 📄 License

This project is provided as-is for educational purposes.

## 🤝 Contributing

This is a demonstration project. For issues or improvements, please refer to the architecture guidelines above.

---

**Created**: December 25, 2025  
**Architecture Pattern**: Clean Architecture with Layered Approach  
**Scalability**: Designed for future enhancements (authentication, pagination, advanced filtering, etc.)
