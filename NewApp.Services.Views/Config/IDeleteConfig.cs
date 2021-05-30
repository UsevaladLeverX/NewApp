using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public interface IDeleteConfig<T> where T : class
    {
        T Config(int id);
    }
}
