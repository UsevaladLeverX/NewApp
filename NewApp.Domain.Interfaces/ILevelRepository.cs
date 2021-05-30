using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Domain.Core;

namespace NewApp.Domain.Interfaces
{
    public interface ILevelRepository
    {
        IEnumerable<Level> GetAll();
        Level Get(int id);
        void Create(Level item);
        void Update(Level item);
        void Delete(int id);
        void Save();
    }
}
