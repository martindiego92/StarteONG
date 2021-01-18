using System;
using System.Collections.Generic;
using System.Linq;
using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;

namespace starteAlkemy.Repository.IRepository
{
    public class HomeRepository : IHomeRepository

    {
        #region Atribute
        private readonly StartContext _dbContext;
        private bool _disposed = false;
        #endregion

        #region Ctor

        public HomeRepository()
        {
            _dbContext = new StartContext();
        }

        public HomeRepository(StartContext startContext)
        {
            _dbContext = startContext;
        }
        #endregion

        #region Methods

        public Home Get()
        {
            Home home = _dbContext.Home.OrderBy(p => p.Id)
                       .FirstOrDefault();
            if (home != null)
            {
                home.ListImage = _dbContext.HomeImages.ToList();
                home.ListProject = _dbContext.Project.Include("ListLinkProject").ToList();
            }
            return home;
        }

        public Home GetIdMP()
        {
            Home home = _dbContext.Home.OrderByDescending(p => p.Id)
                       .FirstOrDefault();

            return home;
        }


        public void Add(Home h)
        {
            _dbContext.Home.Add(h);
            Save();
        }

        public void Edit(Home h)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.HomeImages.RemoveRange(_dbContext.HomeImages.ToList());
                    _dbContext.Home.RemoveRange(_dbContext.Home.ToList());

                    List<HomeImages> homeImages = h.ListImage.ToList();
                    if (homeImages.Count > 0)
                    {
                        foreach (var item in homeImages)
                        {
                            _dbContext.HomeImages.Add(item);
                        }
                    }
                    _dbContext.Home.Add(h);

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
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}