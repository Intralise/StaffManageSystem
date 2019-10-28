using AutoMapper;
using MyCourseWork.AuthorizationService.Dto;
using MyCourseWork.AuthorizationService.Entities;

namespace MyCourseWork.AuthorizationService
{
    internal class MapperProfile: Profile
    {

        public MapperProfile()
            : base("UsersManagement")
        {
            MapEmployeeRegister();
            MapEmployeePhone();
            MapEmployeeLevelDto();
            MapEmployeeDto();
            MapEmployeeAuthfication();
            MapAddingLevelDto();
            MapDepartmentsDto();
        }

        private void MapDepartmentsDto()
        {
            CreateMap<ApplicationsDto, ApplicationsEntity>()
                .ForMember(x => x.AppId, options => options.Ignore())
                .ForMember(x => x.AppHeader, options => options.MapFrom(m => m.AppHeader))
                .ForMember(x => x.Breakage, options => options.MapFrom(m => m.Breakage))
                .ForMember(x => x.Room, options => options.MapFrom(m => m.Room))
                .ForMember(x => x.Date, options => options.MapFrom(m => m.Date))
                .ForMember(x => x.Author, options => options.MapFrom(m => m.Author))
                .ForMember(x => x.Answer, options => options.MapFrom(m => m.Answer ))
                .ReverseMap();
        }

        private void MapEmployeeRegister()
        {
            CreateMap<EmployeeDto, EmployeeEntity>()
                .ForMember(x => x.Birthsday, options => options.MapFrom(m => m.Birthsday))
                .ForMember(x=>x.Email, options => options.MapFrom(m=>m.Email))
                .ForMember(x => x.FirstName , options => options.MapFrom(m => m.FirstName))
                .ForMember(x => x.MiddleName, options => options.MapFrom(m => m.MiddleName))
                .ForMember(x => x.SecondName, options => options.MapFrom(m => m.SecondName))
                .ForMember(x => x.Sex, options => options.MapFrom(m => m.Sex))
                .ForMember(x => x.Education, options => options.MapFrom(m => m.Education))
                .ForMember(x => x.PlaceOfLiving, options => options.MapFrom(m => m.PlaceOfLiving))
                .ForMember(x => x.IdEmployee, options => options.Ignore())
                .ReverseMap();
        }

        private void MapEmployeeDto()
        {
            CreateMap<EmployeeDto, EmployeeEntity>()
                .ForMember(x => x.Birthsday, options => options.MapFrom(m => m.Birthsday))
                .ForMember(x => x.Email, options => options.MapFrom(m => m.Email))
                .ForMember(x => x.FirstName, options => options.MapFrom(m => m.FirstName))
                .ForMember(x => x.MiddleName, options => options.MapFrom(m => m.MiddleName))
                .ForMember(x => x.SecondName, options => options.MapFrom(m => m.SecondName))
                .ForMember(x => x.Sex, options => options.MapFrom(m => m.Sex))
                .ForMember(x => x.Education, options => options.MapFrom(m => m.Education))
                .ForMember(x => x.PlaceOfLiving, options => options.MapFrom(m => m.PlaceOfLiving))
                .ForMember(x => x.IdEmployee, options => options.Ignore())
                .ForMember(x => x.IdLevel, options => options.Ignore())
                .ReverseMap();
        }

        private void MapEmployeeLevelDto()
        {
            CreateMap<RegistrationLevelDto, AcceptLevelEntity>()
                .ForMember(x => x.Level, options => options.MapFrom(m => m.Level))
                .ForMember(x => x.Name, options => options.Ignore())
                .ForMember(x => x.Description, options => options.Ignore())
                .ForMember(x => x.IdLevel, options => options.Ignore())
                .ReverseMap();
        }

        private void MapAddingLevelDto()
        {
            CreateMap<LevelDto, AcceptLevelEntity>()
                .ForMember(x => x.Level, options => options.MapFrom(m => m.Level))
                .ForMember(x => x.Name, options => options.MapFrom(m => m.Name))
                .ForMember(x => x.Description, options => options.MapFrom(m => m.Description))
                .ForMember(x => x.IdLevel, options => options.Ignore())
                .ReverseMap();
        }

        private void MapEmployeePhone()
        {
            CreateMap<PhonesDto, PhonesEntity>()
                .ForMember(x => x.MobilePhone, options => options.MapFrom(m => m.MobilePhone))
                .ForMember(x => x.StationPhone, options => options.MapFrom(m=> m.StationPhone))
                .ForMember(x => x.IdTable, options => options.Ignore())
                .ReverseMap();
        }


        private void MapEmployeeAuthfication()
        {
            CreateMap<EmployeeAuthDto, AuthificationDateEntity>()
                .ForMember(x => x.Login, options => options.MapFrom(m => m.Login))
                .ForMember(x => x.Password, options => options.MapFrom(m => m.Password))
                .ForMember(x => x.TableId, options => options.Ignore())
                .ForMember(x => x.Employee, options => options.Ignore())
                .ReverseMap();
        }

        private void MapLevelDto()
        {
            CreateMap<LevelDto, AcceptLevelEntity>()
                .ForMember(x => x.Description, options => options.MapFrom(m => m.Description))
                .ForMember(x => x.Level, options => options.MapFrom(m => m.Level))
                .ForMember(x => x.Name, options => options.MapFrom(m => m.Name))
                .ForMember(x => x.IdLevel, options => options.Ignore())
                .ReverseMap();
        }
    }
}
