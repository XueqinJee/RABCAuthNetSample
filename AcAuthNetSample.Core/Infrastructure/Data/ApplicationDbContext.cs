using AcAuthNetSample.Core.Domain.Auth.Entities;
using AuthNet.Shard.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Infrastructure.Data {
    public class ApplicationDbContext : DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // 字段转蛇形命名
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (!string.IsNullOrWhiteSpace(entity.GetTableName())) entity.SetTableName(entity.GetTableName()!.ToSnakeCase());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    foreach (var property in foreignKey.Properties)
                    {
                        property.SetColumnName(property.Name.ToSnakeCase());
                    }
                }
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TokenAccess> AccessTokens { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

    }
}
