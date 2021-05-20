using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;

namespace NewApp.Infrastructure.Data
{
    public interface IDDeleteAndDetails<T> where T:class
    {
        T Configure(int id);
    }
}
