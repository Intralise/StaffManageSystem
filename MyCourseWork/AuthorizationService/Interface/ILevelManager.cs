using MyCourseWork.AuthorizationService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCourseWork.AuthorizationService.Interface
{
    interface ILevelManager
    {
        LevelDto GetLevelById(int id);
        LevelDto GetLevelByName(string levelName);
        LevelDto CreateLevel(LevelDto registrationDto);
        void DeleteLevel(LevelDto registrationDto);
        void DeleteLevelById(int id);
    }
}
