using System;
using System.Collections.Generic;
using System.Linq;
using MyCourseWork.AuthorizationService.Entities;
using MyCourseWork.AuthorizationService.Interface;
using MyCourseWork.AuthorizationService.Dto;
using AutoMapper;

namespace MyCourseWork.AuthorizationService
{
    public class EmployeeManager : IEmployeeManager
    {
        public EmployeeManager(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public bool UserExists(int idEmployee)
        {
            return _dbContext.Employee.Any(t => t.IdEmployee == idEmployee);
        }

        public bool PasswordCheck(string password, int id)
        {
            return _dbContext.AuthDate.Any(t => t.Employee.IdEmployee == id && t.Password == password);
        }

        public bool UserExists(string userLogin)
        {
            return _dbContext.AuthDate.Any(t => t.Login == userLogin);
        }

        public IEnumerable<FullEmployeeDto> GetAllUsers()
        {
            //return _dbContext.Employee.Cast<EmployeeDto>();
            List<FullEmployeeDto> allUsers = new List<FullEmployeeDto>();
            allUsers = allUsers.ToList();
            foreach (EmployeeEntity employee in _dbContext.Employee)
            {
                allUsers.Add(GetUserById(employee.IdEmployee));
            }
            return allUsers;
        }

        public int GetUserId(FullEmployeeDto employee)
        {
            return _dbContext.AuthDate.FirstOrDefault(t => t.Login == employee.AuthificationDto.Login).Employee.IdEmployee;
        }

        public List<FullEmployeeDto> SaveChangesForUsersList(List<FullEmployeeDto> employees)
        {
            employees.ForEach(t => ChangeUserInfo(t));
            _dbContext.SaveChanges();
            return employees;
        }

        public void ChangeUserInfo(FullEmployeeDto employee)
        {
            int id = _dbContext.AuthDate.FirstOrDefault(t => t.Login == employee.AuthificationDto.Login).Employee.IdEmployee;
            _dbContext.AuthDate.FirstOrDefault(t => t.Login == employee.AuthificationDto.Login).Password = employee.AuthificationDto.Password;
            _dbContext.AuthDate.FirstOrDefault(t => t.Login == employee.AuthificationDto.Login).Login = employee.AuthificationDto.Login;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).Birthsday = employee.UserDto.Birthsday;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).Education = employee.UserDto.Education;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).Email = employee.UserDto.Email;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).FirstName = employee.UserDto.FirstName;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).Level.Level = employee.LevelDto.Level; 
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).MiddleName = employee.UserDto.MiddleName;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).PlaceOfLiving = employee.UserDto.PlaceOfLiving;
            _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).SecondName = employee.UserDto.SecondName;
            _dbContext.Phones.FirstOrDefault(t => t.Employee == _dbContext.Employee.FirstOrDefault(y => y.Email == employee.UserDto.Email)).MobilePhone = employee.PhonesDto.MobilePhone;
            _dbContext.Phones.FirstOrDefault(t => t.Employee == _dbContext.Employee.FirstOrDefault(y => y.Email == employee.UserDto.Email)).StationPhone = employee.PhonesDto.StationPhone;
            _dbContext.SaveChanges();
        }

        public FullEmployeeDto GetUserById(int id)
        {
            FullEmployeeDto fullDto = new FullEmployeeDto() { UserDto = Mapper.Map<EmployeeDto>(_dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id)) };
            fullDto.PhonesDto = Mapper.Map<PhonesDto>(_dbContext.Phones.FirstOrDefault(t => t.Employee == _dbContext.Employee.FirstOrDefault(y => y.Email == fullDto.UserDto.Email)));
            fullDto.AuthificationDto = Mapper.Map<EmployeeAuthDto>(_dbContext.AuthDate.FirstOrDefault(t => t.Employee.IdEmployee == id));
            fullDto.LevelDto = Mapper.Map<LevelDto>(_dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id).Level);
            if (fullDto == null)
            {
                throw new Exception($"There is no users with id = {id}");
            }
            return fullDto;
        }

        public FullEmployeeDto GetUserByEmail(string Email)
        {
            FullEmployeeDto fullDto = new FullEmployeeDto() { UserDto = Mapper.Map<EmployeeDto>(_dbContext.Employee.FirstOrDefault(t => t.Email == Email)) };
            fullDto.PhonesDto = Mapper.Map<PhonesDto>(_dbContext.Phones.FirstOrDefault(t => t.Employee == _dbContext.Employee.FirstOrDefault(y => y.Email == Email)));
            fullDto.AuthificationDto = Mapper.Map<EmployeeAuthDto>(_dbContext.AuthDate.FirstOrDefault(t => t.Employee.IdEmployee == _dbContext.Employee.FirstOrDefault(y => y.Email == Email).IdEmployee));

            fullDto.LevelDto = Mapper.Map<LevelDto>(_dbContext.AcceptLevel.FirstOrDefault(t => t.IdLevel == fullDto.UserDto.IdLevel));
            if (fullDto == null)
            {
                throw new Exception($"There is no users with Email = {Email}");
            }
            return fullDto;
        }

        public FullEmployeeDto GetUserByLogin(string employeeLogin)
        {
            int id;
            try
            {
                id = _dbContext.AuthDate.FirstOrDefault(t => t.Login == employeeLogin).TableId;
                FullEmployeeDto fullDto = new FullEmployeeDto() { UserDto = Mapper.Map<EmployeeDto>(_dbContext.Employee.FirstOrDefault(t => t.IdEmployee == id)) };
                fullDto.AuthificationDto = Mapper.Map<EmployeeAuthDto>(_dbContext.AuthDate.FirstOrDefault(t => t.Employee.IdEmployee == id));
                fullDto.PhonesDto = Mapper.Map<PhonesDto>(_dbContext.Phones.FirstOrDefault(t => t.Employee == _dbContext.Employee.FirstOrDefault(y => y.Email == fullDto.UserDto.Email)));
                fullDto.LevelDto = Mapper.Map<LevelDto>
                (_dbContext.AcceptLevel.FirstOrDefault(t => t.IdLevel == _dbContext.Employee.FirstOrDefault(y => y.IdEmployee == id).IdLevel));
                return fullDto;
            }
            catch (Exception)
            {
                return null;
                throw new Exception($"There is no users with username = {employeeLogin}");
            }
        }

        public FullEmployeeDto CreateUser(FullEmployeeDto registrationDto)
        {
            EmployeeEntity employeeEntity = new EmployeeEntity()
            {
                IdEmployee = _dbContext.Employee.Count(),
                IdLevel = _dbContext.AcceptLevel.FirstOrDefault(t => t.Level == registrationDto.LevelDto.Level).IdLevel,
                Birthsday = registrationDto.UserDto.Birthsday,
                Email = registrationDto.UserDto.Email,
                FirstName = registrationDto.UserDto.FirstName,
                MiddleName = registrationDto.UserDto.MiddleName,
                SecondName = registrationDto.UserDto.SecondName,
                Sex = registrationDto.UserDto.Sex,
                Education = registrationDto.UserDto.Education,
                PlaceOfLiving = registrationDto.UserDto.PlaceOfLiving
            };
            _dbContext.Phones.Add(new PhonesEntity
            {
                IdTable = _dbContext.Phones.Count(),
                Employee = employeeEntity,
                MobilePhone = registrationDto.PhonesDto.MobilePhone,
                StationPhone = registrationDto.PhonesDto.StationPhone
            });
            _dbContext.Employee.Add(employeeEntity);
            _dbContext.AuthDate.Add(new AuthificationDateEntity
            {
                TableId = _dbContext.AuthDate.Count(),
                Employee = employeeEntity,
                Login = registrationDto.AuthificationDto.Login,
                Password = registrationDto.AuthificationDto.Login
            });
            _dbContext.SaveChanges();
            return GetUserByLogin(registrationDto.AuthificationDto.Login);
        }

        

        public void ChangeUserAcceptLevel(int employeeId, int newLevelId)
        {
            EmployeeEntity userEntity = _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == employeeId);
            if (userEntity == null)
            {
                throw new Exception($"There is no users with id = {employeeId}");
            }

            userEntity.IdLevel = newLevelId;
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int employeeid)
        {

            EmployeeEntity userEntity = _dbContext.Employee.FirstOrDefault(t => t.IdEmployee == employeeid);
            if (userEntity == null)
            {
                throw new Exception($"There is no users with id = {employeeid}");
            }

            _dbContext.Employee.Remove(userEntity);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(FullEmployeeDto employee)
        {

            EmployeeEntity userEntity = _dbContext.Employee.FirstOrDefault(t=>t.IdEmployee == _dbContext.AuthDate.FirstOrDefault(y => y.Login == employee.AuthificationDto.Login).Employee.IdEmployee);
            if (userEntity == null)
            {
                throw new Exception($"There is no users with login = {employee.AuthificationDto.Login}");
            }

            _dbContext.Employee.Remove(userEntity);
            _dbContext.AuthDate.Remove(_dbContext.AuthDate.FirstOrDefault(t => t.Login == employee.AuthificationDto.Login));
            //var phone = _dbContext.Phones.FirstOrDefault(t => t.Employee == userEntity);
            //_dbContext.Phones.Remove(phone); //!
            _dbContext.SaveChanges();
        }

        public EmployeeAuthDto GetEmployeeAuthDateById(int id)
        {
            AuthificationDateEntity employeeEntity = _dbContext.AuthDate.FirstOrDefault(t => t.Employee.IdEmployee == id);
            if (employeeEntity == null)
            {
                throw new Exception($"There is no users with id = {id}");
            }
            return Mapper.Map<EmployeeAuthDto>(employeeEntity);
        }





        private readonly IDbContext _dbContext;
    }

}
