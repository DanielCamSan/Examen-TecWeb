# TecWebFest — Examen (90 minutos)

**Objetivo:** Evaluar dominio de relaciones 1:N, N:M y 1:1 en EF Core y el manejo de arquitectura por capas (Controller ➜ Service ➜ Repository ➜ DbContext) implementando endpoints clave para un mini‑sistema de festival de música.
Entrega: Apellido-Nombre (Camacho-Daniel)
## Contexto del dominio
- Un **Festival** tiene muchas **Stages** (1:N).
- Un **Artist** puede tocar en muchas **Stages**, y una **Stage** recibe muchos **Artists** a través de **Performance**, con horario (N:M con payload).

## Lo que ya tienes listo
Este starter incluye la solución con:
- Capas: `Controllers`, `Services`, `Repositories`, `Data (DbContext)`, `Entities`, `DTOs`.
- DI configurado en `Program.cs` y proveedor **InMemory** para rapidez en aula.
- Repos genéricos y específicos con métodos base y consultas incluídas (`Include`).

## Tareas (implementa y/o completa)
1. **Configurar relaciones en `AppDbContext` (OnModelCreating):**
   - 1:N **Festival ➜ Stages** con FK requerida y eliminación en cascada de stages al borrar un festival.
   - N:M **Artist ↔ Stage** mediante **Performance** con **clave compuesta** `(ArtistId, StageId, StartTime)`.
2. **Validación de solapamiento de horarios:**
   - Al crear un `Performance`, impedir que en **la misma Stage** existan horarios superpuestos.
3. **Endpoints que deben funcionar:**
   - `POST /api/v1/festivals` crea festival con lista de stages.
   - `GET  /api/v1/festivals/{id}/lineup` devuelve programación por stage (ordenada por hora).
   - `POST /api/v1/artists` crea artista.
   - `POST /api/v1/artists/performances` vincula artista con stage y horario.
   - `GET  /api/v1/artists/{id}/schedule` devuelve agenda del artista.
4. **Arquitectura por capas:**
   - No acceder al `DbContext` desde los controllers: usa Services y Repositories (ya están registrados).
   - Mantén DTOs en la capa DTOs, mapea en Services.
5. **Manejo de errores / validaciones**.
## Cómo evaluar y probar
### ✅ 1. Crear un Festival con Stages (1:N)

POST api/v1/festivals
```
{
  "name": "TecWebFest 2025",
  "city": "Cochabamba",
  "startDate": "2025-07-01",
  "endDate": "2025-07-03",
  "stages": [
    { "name": "Main Stage" },
    { "name": "Electro Dome" }
  ]
}
```


### 🎤 2. Crear un Artista

POST api/v1/artists
```
{
  "stageName": "DJ Infinity",
  "genre": "Electronic"
}
```
### 🎸 3. Crear otro Artista
```
{
  "stageName": "Rock Masters",
  "genre": "Rock"
}
```

### 🎭 4. Asignar Performance (N:M con payload)

POST api/v1/artists/performances
```
{
  "artistId": 1,
  "stageId": 1,
  "startTime": "2025-07-01T20:00:00",
  "endTime": "2025-07-01T21:30:00"
}
```

### 📅 5. Consultar Lineup del Festival

GET api/v1/festivals/1/lineup

Respuesta esperada (estructura):
```
{
  "festival": "TecWebFest 2025",
  "city": "Cochabamba",
  "stages": [
    {
      "stage": "Main Stage",
      "performances": [
        {
          "artistId": 1,
          "artist": "DJ Infinity",
          "startTime": "2025-07-01T20:00:00",
          "endTime": "2025-07-01T21:30:00"
        }
      ]
    }
  ]
}
```
### 🎼 6 Ver Agenda de un Artista

GET api/v1/artists/1/schedule


### 🧪 7.  Intentar solapamiento (Debe fallar si validan)

POST api/v1/artists/performances
```
{
  "artistId": 2,
  "stageId": 1,
  "startTime": "2025-07-01T20:30:00",
  "endTime": "2025-07-01T21:00:00"
}
```
Respuesta esperada:
```
{
  "error": "The stage already has a performance in this time range."
}
```

## Notas
- El **foco** es demostrar dominio de **relaciones** y **capas** en 90 minutos.
