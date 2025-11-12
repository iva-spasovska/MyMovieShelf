# MyMovieShelf

**MyMovieShelf** is an ASP.NET Core MVC application built with Onion Architecture. It allows users to create personal movie shelves, add movies from TMDb, track watched movies, and manage reviews. The application demonstrates clean architecture, service-repository patterns, and full-stack ASP.NET Core development.

---

## Features

- User authentication and authorization  
- Personal movie shelves  
- Add movies from TMDb by ID  
- Remove movies from shelf  
- Toggle watched/unwatched status  
- View movie details, including genres and actors  
- CRUD operations for reviews (create, read, update, delete reviews for movies)  
- Responsive UI using Bootstrap  

---

## Architecture

- **Domain Layer** – Entities: `Movie`, `Shelf`, `Review`, `Actor`, `Genre`, `MovieApplicationUser`  
- **Repository Layer** – Generic repository pattern for database access  
- **Service Layer** – Business logic: `MovieService`, `ShelfService`, `ReviewService`, `GenreService`, `ActorService`, `DataFetchService`  
- **Web Layer (MVC)** – Controllers and Views 
- **Onion Architecture** – Each layer depends only on inner layers  

---

## Getting Started

### Prerequisites

- .NET 8 SDK  
- Visual Studio 2022 or VS Code  
- SQL Server / LocalDB  

---

### Setup and Run

1. Clone the repository:

```bash
git clone https://github.com/yourusername/MyMovieShelf.git
cd MyMovieShelf
```

2.Apply the database migrations and set up the database:
```bash
dotnet ef database update --project MyMovieShelf.Web
```

3. Run the application:
```bash
dotnet run --project MyMovieShelf.Web
```

4. Open your browser and navigate to:
```bash
https://localhost:7268
```
