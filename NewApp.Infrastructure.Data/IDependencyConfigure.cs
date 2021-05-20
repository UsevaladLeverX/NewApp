using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;

namespace NewApp.Infrastructure.Data
{
    public interface IDependencyConfigure<T, W> where T: class where W: class
    {
        W Configure(T model, int? id);
        List<SelectListItem> MakeViewPos();
    }
}
