using AppWebBiblioteca.Controllers;
using AppWebBiblioteca.Data;
using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BibliotecaMVC.Tests;

public class LibrosCrearTests
{


    [Fact]
    public async Task Create_ConLibroValido_GuardaElLibroYRedirigeAlIndice()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var libro = new Libro { Title = "Titulo 4", Autor = "Autor 4", Category = "Categoria 4", Imagen = "/Imagenes/descarga (5).jpeg", Price = 12.50 };

        var result = await controller.Create(libro);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        Assert.Equal(1, await context.Libros.CountAsync());
        Assert.Equal("Titulo 4", (await context.Libros.SingleAsync()).Title);
    }

    [Fact]
    public async Task Create_ConModeloInvalido_NoGuardaYDevuelveLaVista()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        controller.ModelState.AddModelError(nameof(Libro.Title), "El titulo es obligatorio.");
        var libro = new Libro { Category = "Categoria 2" };
        var result = await controller.Create(libro);
        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(libro, view.Model);
        Assert.Empty(context.Libros);
    }

    private static LibrosController CreateController(BibliotecaContext context)
    {
        var logger = new Mock<ILogger<LibrosController>>();
        return new LibrosController(context, logger.Object);
    }

    private static BibliotecaContext CreateContext(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .Options;
        return new BibliotecaContext(options);
    }

    [Theory]
    [InlineData("")] 
    [InlineData(null)] 
    public void TituloObligatorio(string? titulo)
    {
        var libro = new Libro { Title = titulo };
        var errores = Validate(libro);

        Assert.Contains(errores, error => error.MemberNames.Contains(nameof(Libro.Title)));
    }

    [Fact]
    public void TituloSinPasarCaracteres()
    {
        var libro = new Libro { Title = new string('L', 101) };
        var errores = Validate(libro);
        Assert.Contains(errores, error => error.MemberNames.Contains(nameof(Libro.Title)));
    }

    private static IReadOnlyCollection<ValidationResult> Validate (Libro libro)
    {
        var context = new ValidationContext(libro);
        var errores = new List<ValidationResult>();
        Validator.TryValidateObject(libro, context, errores, validateAllProperties: true);
        return errores;
    }
}

