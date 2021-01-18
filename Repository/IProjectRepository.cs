using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;
using System.Collections.Generic;

namespace starteAlkemy.Repository
{
    internal interface IProjectRepository : IRepository<Project>
    {
        Project Get(int id);
        List<Project> GetListProject();
        List<LinksMm> GetLinksMm();
    }
}