﻿using System;
using System.Data.Entity;
using System.Linq;
using GForum.Data.Models;
using GForum.Data.Models.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GForum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
            this.Database.Initialize(true);
        }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var entries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IEntity && (
                        (e.State == EntityState.Added) ||
                        (e.State == EntityState.Modified)
                    )
                 );

            foreach (var entry in entries)
            {
                var entity = (IEntity)entry.Entity;
                if (entry.State == EntityState.Added || entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}