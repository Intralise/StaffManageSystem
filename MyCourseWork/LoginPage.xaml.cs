using MyCourseWork.AuthorizationService;
using MyCourseWork.AuthorizationService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCourseWork
{
    public partial class LoginPage : Page
    {
        public LoginPage(EmployeeContext mainContext, Window mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            _mainWindow.Content = this;

            _employeeContext = mainContext;
            _employeeManager = new EmployeeManager(_employeeContext);
            _levelManager = new LevelManager(_employeeContext);
            _generalManager = new GeneralManager(_employeeContext);
            this.Name = "LoginPage";
        }
        
        private void Authification(object sender, RoutedEventArgs e)
        {
            if (_employeeManager.UserExists(LoginBox.Text) && _employeeManager.PasswordCheck(PasswordBox.Text,
                _employeeManager.GetUserId(_employeeManager.GetUserByLogin(LoginBox.Text))))
            {
                UsersListPage userSettingsPage = new UsersListPage(_mainWindow, _employeeContext);
            }
            else
            {
                ErrorLoginMessage.Visibility = _employeeManager.UserExists(LoginBox.Text) ?
                 Visibility.Hidden :
                 Visibility.Visible;

                ErrorPasswordMessage.Visibility = _employeeManager.GetUserByLogin(PasswordBox.Text) != null ?
                    (_employeeManager.PasswordCheck(PasswordBox.Text, 
                    _employeeManager.GetUserId(_employeeManager.GetUserByLogin(LoginBox.Text))) ?
                    Visibility.Hidden :
                    Visibility.Visible) : Visibility.Visible;
            }
        }

        private void CloseApp(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }


        //Create Fake Data
        private void CreateData(object sender, RoutedEventArgs e)
        {
            if (!_generalManager.DataExists())
            {
                _levelManager.CreateLevel(new LevelDto { Description = "Employee", Level = "1", Name = "Employee" });
                _levelManager.CreateLevel(new LevelDto { Description = "Lowest level", Level = "2", Name = "User" });
                _levelManager.CreateLevel(new LevelDto { Description = "Average level", Level = "3", Name = "Senior user" });
                _levelManager.CreateLevel(new LevelDto { Description = "Highest level", Level = "4", Name = "Technical administrator" });

                ApplicationsDto dep = new ApplicationsDto() { AppHeader = "Неисправность", Breakage = "Принтер", Author = "Андрей", Date = "2012.02.23", Room = "4.12", Answer = "Не исправлено" };
                _generalManager.CreateApp(dep);
                dep = new ApplicationsDto() { AppHeader = "Поломка", Breakage = "Принтер", Author = "Андрей", Date = "2012.02.11", Room = "4.12", Answer = "Не исправлено" };
                _generalManager.CreateApp(dep);
                dep = new ApplicationsDto() { AppHeader = "Поломка", Breakage = "Принтер", Author = "Андрей", Date = "2012.02.01", Room = "4.12", Answer = "Не исправлено" };
                _generalManager.CreateApp(dep);

                FullEmployeeDto dto = new FullEmployeeDto();
                dto.AuthificationDto = new EmployeeAuthDto { Login = LoginBox.Text, Password = PasswordBox.Text };
                dto.LevelDto = new LevelDto { Level = "4", Description = "Highest level", Name = "Technical administrator" };
                dto.PhonesDto = new PhonesDto { MobilePhone = "7 926 931 46 88", StationPhone = "9 333 932 93 34" };
                dto.UserDto = new EmployeeDto
                {

                    Birthsday = "06.11.2000",
                    Email = "c1@gmail.com",
                    SecondName = "Усманов",
                    FirstName = "Андрей",
                    MiddleName = "Тимурович",
                    Sex = "М",
                    PlaceOfLiving = "Москва",
                    Education = "Среднее"
                };
                dto.AuthificationDto = new EmployeeAuthDto { Login = "Admin", Password = "Admin" };
                _employeeManager.CreateUser(dto);
            }
        }

        private LevelManager _levelManager;
        private EmployeeManager _employeeManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;
        private GeneralManager _generalManager;
    }
}
