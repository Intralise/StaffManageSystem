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
        }

        private void CreateApp(object sender, RoutedEventArgs e)
        {
            ApplicationsDto dto = new ApplicationsDto() { Room = DepFunction.Text,
                AppHeader = DepName.Text, Breakage = SummaryWorkers.Text, Author = Author.Text, Date = Date.Text, Answer = "Не исправлено" };
            _generalManager.CreateApp(dto);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            _refWindow.Close();
        }

        private GeneralManager _generalManager;
        private EmployeeContext _employeeContext;
        private Window _refWindow;
    }
}
