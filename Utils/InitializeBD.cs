
using System;
using Microsoft.EntityFrameworkCore;

namespace FTCRegex.Utils
{
    public static class InitializeBD
    {
        public static void Initialize(DbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
