using AppWebBiblioteca.Controllers;
using AppWebBiblioteca.Data;
using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaMVC.Tests;

public class LibrosVerTests
{
    [Fact]
    public async Task Index_DevuelveLaListaDeLibros()
    {
        await using var context = CreateContext();

        context.Libros.AddRange(
            new Libro { Title = "Titulo 1", Autor = "Autor 1", Category = "Categoria 1", Imagen = "/Imagenes/descarga (5).jpeg", Price = 12.50 },
            new Libro { Title = "Titulo 2", Autor = "Autor 2", Category = "Categoria 2", Imagen = "/Imagenes/descarga (6).jpeg", Price = 12.50 }
            );
        await context.SaveChangesAsync();
        var controller = CreateController(context);
        var result = await controller.Index();
        var view = Assert.IsType<ViewResult>(result);
        var libros = Assert.IsAssignableFrom<IEnumerable<Libro>>(view.Model);
        Assert.Equal(2, libros.Count());
    }

    [Fact]
    public async Task Details_ConLibroExistente_DevuelveElLibro()
    {
        await using var context = CreateContext();
        var libro = new Libro { Title = "Titulo 3", Autor = "Autor 3", Category = "Categoria 3", Imagen = "/Imagenes/descarga (5).jpeg", Price = 12.50 };
        context.Libros.Add(libro);
        await context.SaveChangesAsync();
        var controller = CreateController(context);

        var result = await controller.Details(libro.Id);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(libro, view.Model);
    }

    [Fact]
    public async Task Details_ConLibroInexistente_DevuelveNotFound()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var result = await controller.Details(999);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ConLibroInexistente_RegistraLogWarning()
    {
        await using var context = CreateContext();
        var loggerMock = new Mock<ILogger<LibrosController>>();
        var controller = new LibrosController(context, loggerMock.Object);

        await controller.Details(999);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("no se ha encontrado")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
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

}