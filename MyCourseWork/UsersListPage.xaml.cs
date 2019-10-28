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
    /// <summary>
    /// Логика взаимодействия для UsersListPage.xaml
    /// </summary>
    public partial class UsersListPage : Page
    {
        public UsersListPage(Window MainWindow, EmployeeContext context)
        {
            InitializeComponent();
            _employeeContext = context;
            _employeeManager = new EmployeeManager(_employeeContext);
            _mainWindow = MainWindow;
            UpdateGridShow();
            _pages = new List<Page>();
            _pages.Add(this);
            this.Name = "UsersPage";
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
            employeesGrid.ItemsSource = users[0];
        }


        private void PhonesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void EmployeesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //При нажатии двойном на таблицу
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //save changes
            List<FullEmployeeDto> employees = new List<FullEmployeeDto>();
            DataGrid dataGrid = new DataGrid();
            dataGrid = (DataGrid)employeesGrid;
            string item = dataGrid.Items[0].ToString();
            for (int i = 0; i < employeesGrid.Items.Count; i++)
            {
                string items = employeesGrid.Items[i].ToString();
                employees.Add(_employeeManager.GetUserByEmail(employeesGrid.Items[i].ToString()));
            }
            _employeeManager.SaveChangesForUsersList(employees);
            UpdateGridShow();
        }

        private List<FullEmployeeDto> _employees;
        private EmployeeManager _employeeManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;
        private List<Page> _pages;

        private void EmployeesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            DataGrid dataGrid = new DataGrid();
            dataGrid = (DataGrid)sender;
            string item = dataGrid.SelectedValue.ToString();
            ReferenceWindow referenceWindow = new ReferenceWindow();
            UserSettingsPage page = new UserSettingsPage(_employeeManager.GetUserByEmail(item),
                referenceWindow, referenceWindow, _employeeContext);
            referenceWindow.Content = page;
            referenceWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReferenceWindow referenceWindow = new ReferenceWindow();
            RegistrationPage page = new RegistrationPage(_employeeContext, referenceWindow);
            referenceWindow.Content = page;
            referenceWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UpdateGridShow();
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DepartmentListPage page = new DepartmentListPage(_mainWindow, _employeeContext);
            _mainWindow.Content = page;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
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

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            LoginPage userSettingsPage = new LoginPage
                (_employeeContext, _mainWindow);
            _mainWindow.Content = userSettingsPage; 
        }

        private void EmployeesGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
