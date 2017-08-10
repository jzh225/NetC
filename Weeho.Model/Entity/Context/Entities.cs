using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Weeho.Model.Entity
{
    public partial class Entities : IContext
    {
        private void UpdateEntity()
        {

            foreach (var item in ChangeTracker.Entries<IEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedTime = DateTime.Now;
                        item.Entity.UpdateTime = DateTime.Now;
                        item.Entity.IsDeleted = false;
                        break;
                    //case EntityState.Deleted:
                    //    item.Entity.UpdatedTime = DateTime.Now;
                    //    item.Entity.IsDeleted = true;
                    //    item.Property("CreatedTime").IsModified = false;
                    //    item.Property("IsDeleted").IsModified = false;
                    //    item.State = EntityState.Modified;
                    //    break;
                    case EntityState.Modified:
                        item.Entity.UpdateTime = DateTime.Now;
                        item.Property("CreatedTime").IsModified = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public override int SaveChanges()
        {
            try
            {
                UpdateEntity();
                return base.SaveChanges();
            }
            //catch (DbUpdateConcurrencyException ex)
            //{

            //}
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }



        public override Task<int> SaveChangesAsync()
        {
            try
            {
                UpdateEntity();
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }
        public int Commit()
        {
            return this.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}
