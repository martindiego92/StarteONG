using starteAlkemy.Models;
using starteAlkemy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace starteAlkemy.Repository.Implements
{
    public class AdminRepository : IAdminRepository
    {

        #region Atribute
        private readonly StartContext _dbContext;
        private bool _disposed = false;
        #endregion

        #region Ctor

        public AdminRepository()
        {
            _dbContext = new StartContext();
        }

        public AdminRepository(StartContext startContext)
        {
            _dbContext = startContext;
        }
        #endregion

        #region Methods

        public Admin Get()
        {
            Admin admin = _dbContext.Admin.OrderByDescending(p => p.Id)
                          .FirstOrDefault();
            return admin;
        }


        public Admin Get( string password, string email)
        {
            Admin admin;
            var lst = from d in _dbContext.Admin
                      where  d.Password == password && d.Email ==email && d.Active==true
                      select d;

            if (lst.Count()>= 1)
            {
                 admin = lst.First();
            }
            else
            {
                admin = null;                
            }
            return admin;
        }

        public void Add(Admin h)
        {
            _dbContext.Admin.Add(h);
            Save();
        }

        public void Edit(Admin h)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Entry(h).State = EntityState.Modified;
                    Save();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();

                }

            }

        }

        public void Delete(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var model = _dbContext.Admin.Find(id);
                    if (model != null) _dbContext.Admin.Remove(model);
                    Save();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    transaction.Rollback();

                }


            }
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

