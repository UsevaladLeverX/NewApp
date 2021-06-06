using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Domain.Core;

namespace NewApp.Domain.Interfaces
{
    public interface IMenteeRepository
    {
        IQueryable<Mentee> GetSource();
        IEnumerable<Mentee> GetAll();
        Mentee Get(int id);
        void Create(Mentee item);
        void Update(Mentee item);
        void Delete(int id);
        void Save();
    }
}
