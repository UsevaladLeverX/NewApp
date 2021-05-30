using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public interface IIndexConfig<T> where T:class 
    {
        public List<T> Config();
    }
}
