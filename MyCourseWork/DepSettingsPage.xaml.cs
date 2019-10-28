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
    /// Логика взаимодействия для DepSettingsPage.xaml
    /// </summary>
    public partial class DepSettingsPage : Page
    {
        public DepSettingsPage(ApplicationsDto activeUser, Window mainWindow, Window referenceWindow, EmployeeContext employeeContext)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _referenceWindow = referenceWindow;
            _activeDep = activeUser;
            _employeeContext = employeeContext;
            _employeeManager = new GeneralManager(_employeeContext);
            Author.Text = _activeDep.Author;
            Brekage.Text = _activeDep.Breakage;
            Room.Text = _activeDep.Room;
            AppHeader.Text = _activeDep.AppHeader;
            Date.Text = _activeDep.Date;
            Answer.Text = _activeDep.Answer;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _activeDep.AppHeader = AppHeader.Text;
            _activeDep.Breakage = Brekage.Text;
            _activeDep.Room = Room.Text;
            _activeDep.Date = Date.Text;
            _activeDep.Author = Author.Text;
            _activeDep.Answer = Answer.Text;
            _employeeManager.ChangeDepInfo(_activeDep);
        }

        private Window _mainWindow;
        private ApplicationsDto _activeDep;
        private EmployeeContext _employeeContext;
        private GeneralManager _employeeManager;
        private Window _referenceWindow;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _referenceWindow.Close();
        }
    }
}
