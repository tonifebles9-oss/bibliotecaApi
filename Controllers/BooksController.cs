using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.Models;
using BibliotecaAPI.Dtos;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> books = new List<Book>
    {
        new Book
        {
            Id = 1,
            Title = "Clean Code",
            Author = "Robert Martin",
            Genre = "Programming",
            Year = 2008,
            Quantity = 5
        },
        new Book
        {
            Id = 2,
            Title = "Harry Potter",
            Author = "J.K. Rowling",
            Genre = "Fantasy",
            Year = 2001,
            Quantity = 10
        }
    };

    // GET TODOS
    [HttpGet]
    public IActionResult GetBooks(string? title)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            var filteredBooks = books
                .Where(b => b.Title.Contains(title,
                StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filteredBooks.Any())
                return NotFound("No se encontraron libros.");

            return Ok(filteredBooks);
        }

        return Ok(books);
    }

    // GET POR ID
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        var book = books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST
    [HttpPost]
    public IActionResult CreateBook(BookCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title) ||
            string.IsNullOrWhiteSpace(dto.Author))
        {
            return BadRequest("Título y Autor son obligatorios.");
        }

        var book = new Book
        {
            Id = books.Max(x => x.Id) + 1,
            Title = dto.Title,
            Author = dto.Author,
            Genre = dto.Genre,
            Year = dto.Year,
            Quantity = dto.Quantity
        };

        books.Add(book);

        return CreatedAtAction(
            nameof(GetBookById),
            new { id = book.Id },
            book);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, BookUpdateDto dto)
    {
        var book = books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(dto.Title) ||
            string.IsNullOrWhiteSpace(dto.Author))
        {
            return BadRequest("Título y Autor son obligatorios.");
        }

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Genre = dto.Genre;
        book.Year = dto.Year;
        book.Quantity = dto.Quantity;

        return Ok(book);
    }

    // PATCH
    [HttpPatch("{id}")]
    public IActionResult PatchBook(int id, BookPatchDto dto)
    {
        var book = books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return NotFound();

        if (dto.Title != null)
            book.Title = dto.Title;

        if (dto.Author != null)
            book.Author = dto.Author;

        if (dto.Genre != null)
            book.Genre = dto.Genre;

        if (dto.Year.HasValue)
            book.Year = dto.Year.Value;

        if (dto.Quantity.HasValue)
            book.Quantity = dto.Quantity.Value;

        return Ok(book);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return NotFound();

        books.Remove(book);

        return Ok("Libro eliminado correctamente.");
    }

    // RESERVAR LIBRO
    [HttpPost("reserve/{id}")]
    public IActionResult ReserveBook(int id)
    {
        var book = books.FirstOrDefault(x => x.Id == id);

        if (book == null)
        {
            return NotFound(new
            {
                Message = "Libro no encontrado"
            });
        }

        if (book.Quantity <= 0)
        {
            return BadRequest(new
            {
                Message = "No hay libros disponibles"
            });
        }

        book.Quantity--;

        return Ok(new
        {
            Message = "Reserva realizada correctamente",
            BookId = book.Id,
            Title = book.Title,
            RemainingBooks = book.Quantity
        });
    }
}