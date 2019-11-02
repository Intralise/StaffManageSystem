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
using MyCourseWork.AuthorizationService.Entities;
using MyCourseWork.AuthorizationService.Dto;


namespace MyCourseWork
{
    public partial class CreatingApplication : Page
    {
        public CreatingApplication(EmployeeContext context, Window refWindow)
        {
            InitializeComponent();

            _employeeContext = context;
            _generalManager = new GeneralManager(_employeeContext);

            _refWindow = refWindow;
            _refWindow.Content = this;

            _validator = new GeneralValidator();

            OnStartup();
        }

        private bool RegistrationDtoCheck()
        {
            ErrorAppMessage.Visibility = DepName.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorAuthorMessage.Visibility = Author.Text.Length > 5 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorDateMessage.Visibility = Date.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorItemMessage.Visibility = Breakage.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;
            ErrorNumberMessage.Visibility = RoomNumber.Text.Length > 2 ?
                Visibility.Hidden : Visibility.Visible;


            if (ErrorAppMessage.Visibility == Visibility.Hidden && ErrorAuthorMessage.Visibility == Visibility.Hidden &&
                ErrorDateMessage.Visibility == Visibility.Hidden && ErrorItemMessage.Visibility == Visibility.Hidden &&
                ErrorNumberMessage.Visibility == Visibility.Hidden) { return true; }
            else { return false; }
        }

        private void CreateApp(object sender, RoutedEventArgs e)
        {
            if (RegistrationDtoCheck())
            {
                ApplicationsDto dto = new ApplicationsDto()
                {
                    Room = RoomNumber.Text,
                    AppHeader = DepName.Text,
                    Breakage = Breakage.Text,
                    Author = Author.Text,
                    Date = Date.Text,
                    Answer = "Не исправлено"
                };
                _generalManager.CreateApp(dto);
            }
        }

        private void OnStartup()
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent, new RoutedEventHandler(GlobalGotFoxus));
        }
        private void GlobalGotFoxus(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Text == "Заголовок заявки" || box.Text == "Неисправный предмет"
                || box.Text == "Номер кабинета" || box.Text == "Автор заявки"
                || box.Text == "Дата заявки" || box.Text == "Дата рождения")
            { box.Text = ""; }
        }

        private void DepName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.RusString)) && e.Text.Length < 10);
        }

        private void SummaryWorkers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Digit)) && e.Text.Length < 4);
        }

        private void DepFunction_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Digit)) && e.Text.Length < 5);
        }

        private void Author_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.RusString)) && e.Text.Length < 16);
        }

        private void Date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((_validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Char)
                || _validator.Validate(e.Text.ToCharArray()[0], ValidateValues.Digit)) && e.Text.Length < 10 );
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            _refWindow.Close();
        }

        private GeneralValidator _validator;
        private GeneralManager _generalManager;
        private EmployeeContext _employeeContext;
        private Window _refWindow;
    }
}
