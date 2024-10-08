﻿using ComexAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ComexAPI.Data; 
public class ProductContext: DbContext {

    public ProductContext(DbContextOptions<ProductContext> opts) : base(opts) {

    }

    protected override void OnModelCreating(ModelBuilder builder) {

        builder.Entity<Produto>()
            .HasOne(produto => produto.Categoria)
            .WithMany(categoria => categoria.Produtos)
            .HasForeignKey(produto => produto.CategoriaId)
             .OnDelete(DeleteBehavior.Restrict);
        ;

        builder.Entity<Endereco>()
            .HasOne(endereco => endereco.Cliente)
            .WithOne(cliente => cliente.Endereco)
            .OnDelete(DeleteBehavior.Restrict);

    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> clientes { get; set; }
    public DbSet<Endereco> enderecos { get; set; }

    public DbSet<Categoria> categorias { get; set; }

}
