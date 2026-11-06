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

public class LibrosEditarTests
{

    [Fact]
    public async Task Edit_ConLibroValido_ActualizaElLibro()
    {
        var databaseName = Guid.NewGuid().ToString();
        int libroId;
        await using (var seedContext = CreateContext(databaseName))
        {
            var libro = new Libro { Title = "Titulo original", Autor = "Autor original", Category = "Categoria original", Imagen = "/Imagenes/descarga (5).jpeg", Price = 12.50 };
            seedContext.Libros.Add(libro);
            await seedContext.SaveChangesAsync();
            libroId = libro.Id;
        }

        await using var context = CreateContext(databaseName);
        var controller = CreateController(context);
        var libroEditado = new Libro
        {
            Id = libroId,
            Title = "titulo actualizado",
            Autor = "Autor original",
            Category = "Categoria original",
            Imagen = "/Imagenes/descarga (5).jpeg",
            Price = 12.50
        };

        var result = await controller.Edit(libroId, libroEditado);


        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        var libroGuardado = await context.Libros.AsNoTracking().SingleAsync();
        Assert.Equal("titulo actualizado", libroGuardado.Title);
    }

    [Fact]
    public async Task Edit_ConIdentificadorDiferente_DevuelveBadRequest()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var libro = new Libro { Id = 1, Title = "Titulo" };
        var result = await controller.Edit(2, libro);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Edit_ConLibroInexistente_DevuelveNotFound()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var libro = new Libro { Id = 999, Title = "Titulo" };
        var result = await controller.Edit(999, libro);
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
