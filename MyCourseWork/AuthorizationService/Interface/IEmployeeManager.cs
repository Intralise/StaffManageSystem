using System.Collections.Generic;
using MyCourseWork.AuthorizationService.Dto;

namespace MyCourseWork.AuthorizationService.Interface
{
    public interface IEmployeeManager
    {
        bool UserExists(int id);
        bool UserExists(string userLogin);
        IEnumerable<FullEmployeeDto> GetAllUsers();
        FullEmployeeDto GetUserById(int id);
        FullEmployeeDto GetUserByLogin(string userLogin);
        FullEmployeeDto CreateUser(FullEmployeeDto registrationDto);
        EmployeeAuthDto GetEmployeeAuthDateById(int employeeId);
        void ChangeUserAcceptLevel(int userId, int newAcceptLevel);
        void DeleteUser(int id);
        void DeleteUser(FullEmployeeDto user);
    }
}
