using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class UsersListPage : Page
    {
        public UsersListPage(Window MainWindow, EmployeeContext context)
        {
            InitializeComponent();

            _mainWindow = MainWindow;
            _mainWindow.Content = this;

            _employeeContext = context;
            _employeeManager = new EmployeeManager(_employeeContext);

            UpdateGridShow();
        }

        public void UpdateGridShow()
        {
            _employees = new List<FullEmployeeDto>();
            _employees = _employeeManager.GetAllUsers().ToList();
            List<EmployeeDtoWithntLvl> users = new List<EmployeeDtoWithntLvl>();
            
            foreach (FullEmployeeDto dto in _employees)
            {
                users.Add(new EmployeeDtoWithntLvl
                {
                    Birthsday = dto.UserDto.Birthsday,
                    Education = dto.UserDto.Education,
                    Email = dto.UserDto.Email,
                    FirstName = dto.UserDto.FirstName,
                    MiddleName = dto.UserDto.MiddleName,
                    PlaceOfLiving = dto.UserDto.PlaceOfLiving,
                    SecondName = dto.UserDto.SecondName,
                    Sex = dto.UserDto.Sex
                });
            }
            employeesGrid.ItemsSource = users;
        }

        private void EmployeesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid = (DataGrid)sender;
            string item = dataGrid.SelectedValue.ToString();
            ReferenceWindow referenceWindow = new ReferenceWindow();
            UserSettingsPage page = new UserSettingsPage(_employeeManager.GetUserByEmail(item), referenceWindow, _employeeContext);
            referenceWindow.Show();
        }

        private void RegistrationPage(object sender, RoutedEventArgs e)
        {
            ReferenceWindow referenceWindow = new ReferenceWindow();
            RegistrationPage page = new RegistrationPage(_employeeContext, _mainWindow, referenceWindow);
            referenceWindow.Content = page;
            referenceWindow.Show();
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid = (DataGrid)employeesGrid;
            if (dataGrid.SelectedValue.ToString() != null)
            {
                string item = dataGrid.SelectedValue.ToString();
                _employeeManager.DeleteUser(_employeeManager.GetUserByEmail(item));
            }
            UpdateGridShow();
        }

        private void UpdateTable(object sender, RoutedEventArgs e)
        {
            UpdateGridShow();
        }

        private void ShowDepartmentsMenu(object sender, RoutedEventArgs e)
        {
            ApplicationsListPage page = new ApplicationsListPage(_mainWindow, _employeeContext);
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            var process = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.DisplayAlerts = false;
            wb = app.Workbooks.Add();
            ws = wb.ActiveSheet;
            employeesGrid.SelectAllCells();
            employeesGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, employeesGrid);
            ws.Paste();
            ws.Range["A1", "H1"].Font.Bold = true;
            int number1 = ws.UsedRange.Rows.Count;
            Microsoft.Office.Interop.Excel.Range myRange = ws.Range["A1", "H" + number1];
            myRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            myRange.WrapText = false;
            ws.Columns.EntireColumn.AutoFit();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            LoginPage userSettingsPage = new LoginPage
                (_employeeContext, _mainWindow);
        }

        private List<FullEmployeeDto> _employees;
        private EmployeeManager _employeeManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;
        private List<Page> _pages;

    }
}
