using System;
using System.Collections.Generic;
using System.Linq;
using MyCourseWork.AuthorizationService.Entities;
using MyCourseWork.AuthorizationService.Interface;
using MyCourseWork.AuthorizationService.Dto;
using MyCourseWork.AuthorizationService;
using AutoMapper;


namespace MyCourseWork
{
    public class GeneralManager
    {
        public GeneralManager(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ApplicationsDto> GetAllDepartments()
        {
            List<ApplicationsDto> allDep = new List<ApplicationsDto>();
            foreach (ApplicationsEntity dep in _dbContext.Applications)
            {
                allDep.Add(GetDepById(dep.AppId));
            }
            return allDep;
        }

        public ApplicationsDto GetDepById(int id)
        {
            ApplicationsDto dto = new ApplicationsDto();
            dto = Mapper.Map<ApplicationsDto>(_dbContext.Applications.FirstOrDefault(t => t.AppId == id));
            if (dto == null)
            {
                throw new Exception($"There is no departments with id = {id}");
            }
            return dto;
        }

        public ApplicationsDto GetDepByName(string id)
        {
            ApplicationsDto dto = new ApplicationsDto();
            dto = Mapper.Map<ApplicationsDto>(_dbContext.Applications.FirstOrDefault(t => t.AppHeader == id));
            if (dto == null)
            {
                throw new Exception($"There is no departments with id = {id}");
            }
            return dto;
        }

        public ApplicationsDto CreateDepartment(ApplicationsDto registrationDto)
        {
            ApplicationsEntity depEntity = new ApplicationsEntity()
            {
                AppId = _dbContext.Employee.Count(),
                AppHeader = registrationDto.AppHeader,
                Breakage = registrationDto.Breakage,
                Author = registrationDto.Author,
                Date = registrationDto.Date,
                Room = registrationDto.Room,
                Answer = registrationDto.Answer
            };
            _dbContext.Applications.Add(depEntity);
            _dbContext.SaveChanges();
            return GetDepById(depEntity.AppId);

        }

        public void DeleteDep(string name)
        {
            ApplicationsEntity depEntity = _dbContext.Applications.FirstOrDefault(t => t.AppHeader == name);
            if (depEntity == null)
            {
                throw new Exception($"There is no dep with name = {depEntity.AppHeader}");
            }

            _dbContext.Applications.Remove(depEntity);
            _dbContext.SaveChanges();
        }

        public void ChangeDepInfo(ApplicationsDto dep)
        {
            int id = _dbContext.Applications.FirstOrDefault(t => t.AppHeader == dep.AppHeader).AppId;
            _dbContext.Applications.FirstOrDefault(t => t.AppId == id).Room = dep.Room;
            _dbContext.Applications.FirstOrDefault(t => t.AppId == id).AppHeader = dep.AppHeader;
            _dbContext.Applications.FirstOrDefault(t => t.AppId == id).Breakage = dep.Breakage;
            _dbContext.Applications.FirstOrDefault(t => t.AppId == id).Author = dep.Author;
            _dbContext.Applications.FirstOrDefault(t => t.AppId == id).Date = dep.Date;
            _dbContext.Applications.FirstOrDefault(t => t.AppId == id).Answer = dep.Answer;
            _dbContext.SaveChanges();
        }

        private readonly IDbContext _dbContext;
    }
}
