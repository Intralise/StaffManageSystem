using MyCourseWork.AuthorizationService;
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyCourseWork.AuthorizationService.Entities;
using MyCourseWork.AuthorizationService.Dto;
using MyCourseWork;

namespace MyCourseWork
{
    public partial class UserCreatingPage : Page
    {
        public UserCreatingPage(EmployeeContext context, Window mainWindow, Window referencesWindow)
        {
            _employeeContext = context;
            _employeeManager = new EmployeeManager(_employeeContext);
            InitializeComponent();
            _mainWindow = mainWindow;

            _referencesWindow = referencesWindow;
            _referencesWindow.Content = this;

            _validator = new GeneralValidator();

            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent, new RoutedEventHandler(GlobalGotFoxus));
        }

        private void WindoClose(object sender, RoutedEventArgs e)
        {
            _referencesWindow.Close();
        }

        //Регистрация
        private void DataCheck(object sender, RoutedEventArgs e)
        {
            if (RegistrationDtoCheck())
            {
                RegistrationNewUser();
            }
        }

        private void RegistrationNewUser()
        {
            FullEmployeeDto dto = new FullEmployeeDto();
            dto.AuthificationDto = new EmployeeAuthDto { Login = LoginBox.Text, Password = PasswordBox.Text };
            dto.LevelDto = new LevelDto { Level = "2", Name = "User", Description = "Lowest level" };
            dto.PhonesDto = new PhonesDto { MobilePhone = "", StationPhone = "" };
            dto.UserDto = new EmployeeDto { Birthsday = BirthsdayBox.Text,
                Email = EmailBox.Text, FirstName = FirstNameBox.Text,
                MiddleName = MiddleNameBox.Text, SecondName = SecondNameBox.Text,
                Sex = SexBox.Text, Education = "Не назначено", PlaceOfLiving = "Не назначено" };
            _employeeManager.CreateUser(dto);
        }


        private bool RegistrationDtoCheck()
        {
            ErrorLoginMessage.Visibility =  LoginBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorPasswordMessage.Visibility = PasswordBox.Text.Length > 5 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorSecondNameMessage.Visibility = SecondNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorFirstNameMessage.Visibility =  FirstNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorMiddleNameMessage.Visibility = MiddleNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            BirthsdayDateErrorMessage.Visibility = BirthsdayBox.Text.Contains(".") 
                &&  BirthsdayBox.Text.Length > 7 ?
                Visibility.Hidden : Visibility.Visible;
            EmailErrorMessage.Visibility = EmailBox.Text.Contains(".") 
                && EmailBox.Text.Contains("@") 
                && EmailBox.Text.Length < 24 
                && EmailBox.Text.Length > 4
               && SexBox.Text.Length > 0 ?
                Visibility.Hidden : Visibility.Visible;

            if (ErrorLoginMessage.Visibility == Visibility.Hidden && ErrorPasswordMessage.Visibility == Visibility.Hidden &&
                EmailErrorMessage.Visibility == Visibility.Hidden && BirthsdayDateErrorMessage.Visibility == Visibility.Hidden &&
                ErrorMiddleNameMessage.Visibility == Visibility.Hidden && ErrorFirstNameMessage.Visibility == Visibility.Hidden &&
                ErrorSecondNameMessage.Visibility == Visibility.Hidden) { return true; }
            else { return false; }
        }

        private void OnStartup(StartupEventArgs e)
        {

        }
        private void GlobalGotFoxus(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "Логин" || box.Text == "Пароль"
                || box.Text == "Фамилия" || box.Text == "Имя"
                || box.Text == "Отчество" || box.Text == "Дата рождения"
                || box.Text == "E-mail")
            { box.Text = ""; }
        }

        private void LoginBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.EngString)) && e.Text.Length < 16);
        }

        private void PasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.EngString)) && e.Text.Length < 16);
        }

        private void Names_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.RusString)) && e.Text.Length < 16) ;
        }

        private void Birthsday_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.RusString) 
                || _validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Char)) && e.Text.Length < 10);
        }

        private void Email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.RusString)
                || _validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Char));
        }

        private void SexBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var c = e.Text.ToCharArray();
            e.Handled = !(c[0] == 'М' || c[0] == 'Ж');
        }

        private EmployeeManager _employeeManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;
        private Window _referencesWindow;
        private GeneralValidator _validator;


    }
}
