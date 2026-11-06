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

public class LibrosEliminarTests
{
    [Fact]
    public async Task Delete_ConLibroExistente_EliminaElLibroYRedirigeAlIndice()
    {
        await using var context = CreateContext();
        var libro = new Libro { Title = "Titulo para eliminar", Autor = "Autor 4", Category = "Categoria 4", Imagen = "/Imagenes/descarga (5).jpeg", Price = 12.50 };
        context.Libros.Add(libro);
        await context.SaveChangesAsync();
        var controller = CreateController(context);
        var result = await controller.Delete(libro.Id);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        Assert.Empty(context.Libros);
    }

    [Fact]

    public async Task Delete_ConLibroInexistente_DevuelveNotFound()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var result = await controller.Delete(999);
        Assert.IsType<NotFoundResult>(result);
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