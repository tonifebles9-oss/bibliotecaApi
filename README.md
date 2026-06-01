# Biblioteca API

## Descripción

Biblioteca API es una aplicación Web API desarrollada en ASP.NET Core que permite gestionar una colección de libros en memoria.

La API permite:

* Consultar todos los libros.
* Consultar un libro por ID.
* Buscar libros por título.
* Crear nuevos libros.
* Actualizar libros completamente (PUT).
* Actualizar parcialmente libros (PATCH).
* Eliminar libros.
* Reservar libros disponibles.

## Tecnologías utilizadas

* ASP.NET Core Web API
* C#
* Swagger
* Postman

## Estructura del proyecto

* Models

  * Book.cs

* Dtos

  * BookCreateDto.cs
  * BookUpdateDto.cs
  * BookPatchDto.cs

* Controllers

  * BooksController.cs

## Endpoints disponibles

### Obtener todos los libros

GET /api/books

### Obtener libro por ID

GET /api/books/{id}

### Buscar libros por título

GET /api/books?title=Harry

### Crear libro

POST /api/books

### Actualizar libro

PUT /api/books/{id}

### Actualización parcial

PATCH /api/books/{id}

### Eliminar libro

DELETE /api/books/{id}

### Reservar libro

POST /api/books/reserve/{id}

## Códigos HTTP implementados

* 200 OK
* 201 Created
* 400 Bad Request
* 404 Not Found

## Autor

Alexander Febles de Aza
