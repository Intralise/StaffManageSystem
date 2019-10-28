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
    /// <summary>
    /// Логика взаимодействия для CreatingDep.xaml
    /// </summary>
    public partial class CreatingDep : Page
    {
        public CreatingDep(EmployeeContext context, Window mainWindow)
        {
            _employeeContext = context;
            _generalManager = new GeneralManager(_employeeContext);
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ApplicationsDto dto = new ApplicationsDto() { Room = DepFunction.Text,
                AppHeader = DepName.Text, Breakage = SummaryWorkers.Text, Author = Author.Text, Date = Date.Text, Answer = "Не исправлено" };
            _generalManager.CreateDepartment(dto);
        }
        private GeneralManager _generalManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }

        private void DepFunction_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
