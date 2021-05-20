using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;


namespace NewApp.Infrastructure.Data
{
    public interface DILists<T> where T: class
    {
        List<T> Configure();
    }
}
