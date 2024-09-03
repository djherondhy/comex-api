using ComexAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ComexAPI.Data; 
public class ProductContext: DbContext {

    public ProductContext(DbContextOptions<ProductContext> opts) : base(opts) {

    }

    public DbSet<Produto> Produtos { get; set; }

}
