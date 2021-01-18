using starteAlkemy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace starteAlkemy.Repository.IRepository
{
    public class ProjectRepository : IProjectRepository
    {
        #region Atribute

        private readonly StartContext _dbContext;
        private bool _disposed = false;
        #endregion
        #region Ctor

        public ProjectRepository()
        {
            _dbContext = new StartContext();
        }

        public ProjectRepository(StartContext startContext)
        {
            _dbContext = startContext;
        }
        #endregion

        public Project Get()
        {
            return _dbContext.Project
                .Include("ListLinkProject")
                .FirstOrDefault();
        }

        public List<Project> GetListProject()
        {
            return _dbContext.Project.OrderByDescending(i => i.DateProject)
                .Include("ListLinkProject")
                .ToList();
        }

        public Project Get(int id)
        {
            Project p = _dbContext.Project.Find(id);
            if (p != null)
            {
                p.ListLinkProject = _dbContext.LinksMm.Where(x => x.ProjectId == id).ToList();
            }
            return p;
        }

        public void Add(Project h)
        {
            _dbContext.Project.Add(h);
            Save();
        }

        public void Edit(Project p)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try

                {   if(p.ListLinkProject.ToList() !=null)
                    {
                        var list = _dbContext.LinksMm.Where(x => x.ProjectId == p.Id).ToList();
                        _dbContext.LinksMm.RemoveRange(list);
                         list = p.ListLinkProject.ToList();
                   
                    foreach (var item in list)
                    {
                            _dbContext.LinksMm.Add(item);
                    }
                    }
                    _dbContext.Entry(p).State = EntityState.Modified;
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
                    var p = _dbContext.Project.Find(id);
                    var images = _dbContext.LinksMm.Where(x => x.ProjectId == p.Id).ToList();
                    foreach (var item in images)
                    {
                        _dbContext.LinksMm.Remove(item);
                    }
                    if (p != null) _dbContext.Project.Remove(p);
                    Save();
                    transaction.Commit();
                }
                catch (Exception )
                {
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

        //Get all LinksMm objects
        public List<LinksMm> GetLinksMm()
        {
            return _dbContext.LinksMm.ToList();
        }
    }
}