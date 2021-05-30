using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public interface ICreateConfig<T, W> where T:class where W:class
    {
        List<SelectListItem> Positions();
        T Config(W model);
    }
}
