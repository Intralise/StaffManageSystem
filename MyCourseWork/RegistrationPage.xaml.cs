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

namespace MyCourseWork
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage(EmployeeContext context, Window mainWindow)
        {
            _employeeContext = context;
            _employeeManager = new EmployeeManager(_employeeContext);
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }

        //Регистрация
        private void Button_Click_2(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(_employeeContext, _mainWindow);
            _mainWindow.Content = loginPage;
        }

        private bool RegistrationDtoCheck()
        {
            ErrorLoginMessage.Visibility = LoginBox.Text.Length < 16 && LoginBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorPasswordMessage.Visibility = PasswordBox.Text.Length < 16 && PasswordBox.Text.Length > 5 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorSecondNameMessage.Visibility = SecondNameBox.Text.Length < 16 && SecondNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorFirstNameMessage.Visibility = FirstNameBox.Text.Length < 16 && FirstNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorMiddleNameMessage.Visibility = MiddleNameBox.Text.Length < 16 && MiddleNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            BirthsdayDateErrorMessage.Visibility = BirthsdayBox.Text.Contains(".") 
                && BirthsdayBox.Text.Length < 10 && BirthsdayBox.Text.Length > 7 ?
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



        private EmployeeManager _employeeManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void LoginBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            KeyConverter d = new KeyConverter();
            var c = e.Text.ToCharArray();
            e.Handled = !(Char.IsDigit(c[0]) || (c[0] >= 'a' && c[0] <= 'z' || c[0] >= 'A' && c[0] <= 'Z'));
        }

        private void Names_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            KeyConverter d = new KeyConverter();
            var c = e.Text.ToCharArray();
            e.Handled = !(c[0] >= 'а' && c[0] <= 'я' || c[0] >= 'А' && c[0] <= 'Я');
        }

        private void Birthsday_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            KeyConverter d = new KeyConverter();
            var c = e.Text.ToCharArray();
            e.Handled = !(Char.IsDigit(c[0]) || c[0] == '.');
        }

        private void Email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            KeyConverter d = new KeyConverter();
            var c = e.Text.ToCharArray();
            e.Handled = !(Char.IsDigit(c[0]) || (c[0] >= 'a' && c[0] <= 'z' || c[0] >= 'A' && c[0] <= 'Z') || (c[0] == '.' || c[0] == '@'));
        }

        private void SexBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            KeyConverter d = new KeyConverter();
            var c = e.Text.ToCharArray();
            e.Handled = !(c[0] == 'М' || c[0] == 'Ж');
        }

        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "Логин") { LoginBox.Text = ""; }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "Пароль") { PasswordBox.Text = ""; }
        }

        private void SecondNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SecondNameBox.Text == "Фамилия") { SecondNameBox.Text = ""; }
        }
       
        private void FirstNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FirstNameBox.Text == "Имя") { FirstNameBox.Text = ""; }
        }

        private void MiddleNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MiddleNameBox.Text == "Отчество") { MiddleNameBox.Text = ""; }
        }

        private void BirthsdayBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BirthsdayBox.Text == "Дата рождения") { BirthsdayBox.Text = ""; }
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "E-mail") { EmailBox.Text = ""; }
        }

    }
}
