using Microsoft.AspNetCore.Mvc.Rendering;
using NewApp.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewApp.Services.Views.IMapper
{
    public interface IMyMapper
    {
        List<SelectListItem> ListOfPositions();
        Level CreateLevelConfig(CreateLevelView model);
        Mentee CreateMenteeConfig(CreateMenteeView model);
        DeleteLevelView DeleteLevelConfig(int id);
        DeleteMenteeView DeleteMenteeConfig(int id);
        EditLevelView EditLevelConfig_1(int? id);
        Level EditLevelConfig_2(EditLevelView model);
        EditMenteeView EditMenteeConfig_1(int? id);
        Mentee EditMenteeConfig_2(EditMenteeView model);
        List<IndexLevelView> IndexLevelConfig();
        List<IndexMenteeView> IndexMenteeConfig();
    }
}
