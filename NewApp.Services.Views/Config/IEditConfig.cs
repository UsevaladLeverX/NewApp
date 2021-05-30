using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views
{
    public interface IEditConfig<T, W> where T:class where W:class
    {
        List<SelectListItem> Positions();
        T Config_1(int? id);
        W Config_2(T model);
    }
}
