using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace proba
{
    public class CustomerDb : DbContext
    {
        public CustomerDb(DbContextOptions<CustomerDb> options)
        : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
    }
}

