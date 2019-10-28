using MyCourseWork.AuthorizationService.Dto;
using MyCourseWork.AuthorizationService.Interface;
using MyCourseWork.AuthorizationService.Entities;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MyCourseWork.AuthorizationService
{
    class LevelManager : ILevelManager
    {

        public LevelManager(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsLevelsExists()
        {
            try
            {
                List<LevelDto> allDep = new List<LevelDto>();
                foreach (AcceptLevelEntity dep in _dbContext.AcceptLevel)
                {
                    allDep.Add(GetLevelById(dep.IdLevel));
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public LevelDto CreateLevel(LevelDto registrationDto)
        {
            if (!_dbContext.AcceptLevel.Any(t => t.Name == registrationDto.Name))
            {
                _dbContext.AcceptLevel.Add(Mapper.Map<AcceptLevelEntity>(registrationDto));
                _dbContext.SaveChanges();
                return registrationDto;
            }
            throw new Exception($"In Db already has this level '{registrationDto.Name}'");
        }

        public void DeleteLevel(LevelDto registrationDto)
        {
            if (_dbContext.AcceptLevel.Any(t => t.Name == registrationDto.Name))
            {
                _dbContext.AcceptLevel.Remove(Mapper.Map<AcceptLevelEntity>(registrationDto));
                _dbContext.SaveChanges();
            }
            throw new Exception($"The database does'nt have this level '{registrationDto.Name}'");
        }

        public void DeleteLevelById(int id)
        {
            if (_dbContext.AcceptLevel.Any(t => t.IdLevel == id))
            {
                _dbContext.AcceptLevel.Remove(_dbContext.AcceptLevel.First(t => t.IdLevel == id));
                _dbContext.SaveChanges();
            }
            throw new Exception($"The database does'nt have this level '{id}'");
        }

        public LevelDto GetLevelById(int id)
        {
            if (_dbContext.AcceptLevel.Any(t => t.IdLevel == id))
            { return Mapper.Map<LevelDto>(_dbContext.AcceptLevel.First(t => t.IdLevel == id)); }
            throw new Exception($"The database does'nt have this level '{id}'");
        }

        public LevelDto GetLevelByName(string levelName)
        {
            if (_dbContext.AcceptLevel.Any(t => t.Name == levelName))
            {
                return Mapper.Map<LevelDto>(_dbContext.AcceptLevel.First(t => t.Name == levelName));
            }
            throw new Exception($"In Db already has this level '{levelName}'");
        }

        private IDbContext _dbContext;
    }
}
