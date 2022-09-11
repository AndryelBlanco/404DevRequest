using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _404DevRequest.Models;

namespace _404DevRequest.Data
{
    public class _404DevRequestContext : DbContext
    {
        public _404DevRequestContext (DbContextOptions<_404DevRequestContext> options)
            : base(options)
        {
        }

        public DbSet<_404DevRequest.Models.TodoItem> TodoItem { get; set; } = default!;
    }
}
