
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using FTCRegex.Models;
namespace FTCRegex.Utils
{
    public static class InitializeBD
    {
        public static void Initialize(DbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
        public static void Initialize(TagContext context)
        {
            context.Database.EnsureCreated();
                if(!context.Groups.Any())
                {
                    context.Groups.Add(new Group(){ Name = "Default" });
                }
            context.SaveChanges();
        }
    }
}
