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
using MyCourseWork.AuthorizationService;
using MyCourseWork.AuthorizationService.Dto;

namespace MyCourseWork
{
    /// <summary>
    /// Логика взаимодействия для UserSettingsPage.xaml
    /// </summary>
    public partial class UserSettingsPage : Page
    {
        public UserSettingsPage(FullEmployeeDto activeUser, Window referenceWindow, EmployeeContext employeeContext)
        {
            InitializeComponent();

            _referenceWindow = referenceWindow;
            _referenceWindow.Content = this;

            _activeUser = activeUser;
            _employeeContext = employeeContext;
            _employeeManager = new EmployeeManager(_employeeContext);

            _validator = new GeneralValidator();

            TextInitialization();
        }

        private void TextInitialization()
        {
            TitleBox.Text = $"Личные данные пользователя по имени {_activeUser.UserDto.FirstName}!";
            FirstNameBox.Text = _activeUser.UserDto.FirstName;
            SecondNameBox.Text = _activeUser.UserDto.SecondName;
            MiddleNameBox.Text = _activeUser.UserDto.MiddleName;
            BirthsdayBox.Text = _activeUser.UserDto.Birthsday;
            EducationBox.Text = _activeUser.UserDto.Education;
            EmailBox.Text = _activeUser.UserDto.Email;
            PasswordBox.Text = _activeUser.AuthificationDto.Password;
            PlaceOfLivingBox.Text = _activeUser.UserDto.PlaceOfLiving;
            MobilePhoneBox.Text = _activeUser.PhonesDto.MobilePhone;
            HomePhoneBox.Text = _activeUser.PhonesDto.StationPhone;
        }

        
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            _referenceWindow.Close();
        }

    private void DataCheck(object sender, RoutedEventArgs e)
        {
            ErrorPasswordMessage.Visibility =  PasswordBox.Text.Length > 3?
                Visibility.Hidden : Visibility.Visible;
            ErrorSecondNameMessage.Visibility = SecondNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorFirstNameMessage.Visibility =  FirstNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorMiddleNameMessage.Visibility =  MiddleNameBox.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorBirthsdayMessage.Visibility = BirthsdayBox.Text.Contains(".") && BirthsdayBox.Text.Length < 10 && BirthsdayBox.Text.Length > 7 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorEmailMessage.Visibility = EmailBox.Text.Contains(".") && EmailBox.Text.Contains("@")
                && EmailBox.Text.Length < 24 && EmailBox.Text.Length > 4
                ? Visibility.Hidden : Visibility.Visible;
            ErrorMobilePhoneMessage.Visibility =  MobilePhoneBox.Text.Length > 8 
                ? Visibility.Hidden : Visibility.Visible;
            ErrorStationPhoneMessage.Visibility =  HomePhoneBox.Text.Length > 8
                ? Visibility.Hidden : Visibility.Visible;
            ErrorPlaceOfLivindMessage.Visibility = PlaceOfLivingBox.Text.Length > 3
                ? Visibility.Hidden : Visibility.Visible;
            ErrorEducationMessage.Visibility = EducationBox.Text.Length > 3
                ? Visibility.Hidden : Visibility.Visible;

            if (ErrorEducationMessage.Visibility == Visibility.Hidden && ErrorPasswordMessage.Visibility == Visibility.Hidden &&
                ErrorPlaceOfLivindMessage.Visibility == Visibility.Hidden && ErrorStationPhoneMessage.Visibility == Visibility.Hidden &&
                ErrorMobilePhoneMessage.Visibility == Visibility.Hidden && ErrorEmailMessage.Visibility == Visibility.Hidden &&
                ErrorMiddleNameMessage.Visibility == Visibility.Hidden && ErrorFirstNameMessage.Visibility == Visibility.Hidden &&
                ErrorSecondNameMessage.Visibility == Visibility.Hidden && ErrorBirthsdayMessage.Visibility == Visibility.Hidden)
            { InformationChange(); }
        }

        public void InformationChange()
        {
            _activeUser.AuthificationDto.Password = PasswordBox.Text;
            _activeUser.PhonesDto.MobilePhone = MobilePhoneBox.Text;
            _activeUser.PhonesDto.StationPhone = HomePhoneBox.Text;
            _activeUser.UserDto.Birthsday = BirthsdayBox.Text;
            _activeUser.UserDto.Education = EducationBox.Text;
            _activeUser.UserDto.Email = EmailBox.Text;
            _activeUser.UserDto.FirstName = FirstNameBox.Text;
            _activeUser.UserDto.MiddleName = MiddleNameBox.Text;
            _activeUser.UserDto.PlaceOfLiving = PlaceOfLivingBox.Text;
            _activeUser.UserDto.SecondName = SecondNameBox.Text;
            _employeeManager.ChangeUserInfo(_activeUser);
            
        }

        private void LoginBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.EngString)) && e.Text.Length < 16);
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Digit)) && e.Text.Length < 16);
        }

        private void Names_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.RusString)) && e.Text.Length < 16);
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

        private GeneralValidator _validator;
        private Window _mainWindow;
        private FullEmployeeDto _activeUser;
        private EmployeeContext _employeeContext;
        private EmployeeManager _employeeManager;
        private Window _referenceWindow;
    }
}
