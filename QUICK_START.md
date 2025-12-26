# ⚡ Quick Start Guide

## 30-Second Setup

### Terminal 1 - Backend API
```bash
cd "d:\MY PROJECTS\MY\Voting App"
dotnet run --project src/Voting.Api/Voting.Api.csproj
```
✅ API runs on `http://localhost:5000`

### Terminal 2 - Frontend
```bash
cd "d:\MY PROJECTS\MY\Voting App\frontend"
npm install
npm start
```
✅ App runs on `http://localhost:4200`

### Open Browser
Navigate to: **`http://localhost:4200`**

---

## What You Can Do

1. **View Sample Data** - 4 candidates and 5 voters are pre-loaded
2. **Add New Candidate** - Click + button on candidates table
3. **Add New Voter** - Click + button on voters table
4. **Cast a Vote** - Select voter and candidate, click "Vote!"
5. **Watch Real-time Updates** - Vote count and "Has Voted" status update instantly

---

## Sample Data

**Candidates:** Alice Johnson, Bob Smith, Carol White, David Brown

**Voters:** Emma Davis, Frank Miller, Grace Lee, Henry Taylor, Iris Martinez

---

## Testing

```bash
# Run backend tests
dotnet test tests/Voting.Application.Tests/

# Results:
# ✅ 5 tests pass covering one-vote-per-voter enforcement
```

---

## API Documentation

**Swagger UI**: `http://localhost:5000/swagger`

Try endpoints directly from the Swagger interface.

---

## Troubleshooting

### Port Already in Use?
```bash
# Change backend port in src/Voting.Api/appsettings.json
# Change frontend port: ng serve --port 4300
```

### npm install slow?
```bash
npm install --legacy-peer-deps
```

### Database issues?
```bash
# Delete voting.db file and restart API
rm "src\Voting.Api\voting.db"
dotnet run --project src/Voting.Api/Voting.Api.csproj
```

---

## Architecture Overview

```
┌─────────────────┐
│   Browser       │
│  (Angular 17)   │
└────────┬────────┘
         │ HTTP + WebSocket
         ↓
┌─────────────────────────────────┐
│    ASP.NET Core 8 API           │
│  - Controllers                  │
│  - SignalR Hub                  │
│  - Use Cases                    │
└────────┬────────────────────────┘
         │ EF Core
         ↓
    ┌─────────┐
    │ SQLite  │
    └─────────┘
```

---

## Key Features

✅ **One Vote Per Voter** - Enforced at DB + app level  
✅ **Real-time Updates** - Via SignalR WebSockets  
✅ **Clean Architecture** - Layered, testable design  
✅ **Validation** - Client + server side  
✅ **Error Handling** - User-friendly messages  
✅ **Material Design** - Professional UI  
✅ **Responsive** - Works on mobile + desktop  

---

## Files Structure

```
VotingApp/
├── src/
│   ├── Voting.Api/          ← REST API & SignalR
│   ├── Voting.Application/  ← Business Logic
│   ├── Voting.Domain/       ← Entities
│   └── Voting.Infrastructure/ ← Database
├── tests/                   ← Unit Tests
├── frontend/                ← Angular App
├── README.md                ← Full Documentation
└── VotingApp.sln           ← Solution File
```

---

## Learn More

- **Full README**: See `README.md`
- **Implementation Details**: See `IMPLEMENTATION_SUMMARY.md`
- **Tests**: See `tests/Voting.Application.Tests/UseCases/Votes/CastVoteUseCaseTests.cs`

---

**Happy Voting! 🗳️**
