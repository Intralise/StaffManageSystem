﻿using System;
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
    /// Логика взаимодействия для DepartmentListPage.xaml
    /// </summary>
    public partial class ApplicationsListPage : Page
    {
        public ApplicationsListPage(Window MainWindow, EmployeeContext context)
        {
            InitializeComponent();
            _employeeContext = context;
            _depManager = new GeneralManager(_employeeContext);
            _mainWindow = MainWindow;
            _mainWindow.Content = this;

            UpdateGridShow();
        }

        public void UpdateGridShow()
        {
            _dep = new List<ApplicationsDto>();
            _dep = _depManager.GetAllDepartments().ToList();
            ApplicationsGrid.ItemsSource = _dep;
        }

        private void EmployeesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            DataGrid dataGrid = new DataGrid();
            dataGrid = (DataGrid)sender;
            string item = ApplicationsGrid.SelectedValue.ToString();
            ReferenceWindow referenceWindow = new ReferenceWindow();
            ApplicationSettingPage page = new ApplicationSettingPage(_depManager.GetDepByName(item), referenceWindow, _employeeContext);
            referenceWindow.Content = page;
            referenceWindow.Show();
        }

        private void CreateApplication(object sender, RoutedEventArgs e)
        {
            ReferenceWindow referenceWindow = new ReferenceWindow();
            CreatingApplication page = new CreatingApplication(_employeeContext, referenceWindow);
            referenceWindow.Content = page;
            referenceWindow.Show();
        }

        private void AppClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DeleteApplication(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid = (DataGrid)ApplicationsGrid;
            if (dataGrid.SelectedValue.ToString() != null)
            {
                string item = dataGrid.SelectedValue.ToString();
                _depManager.DeleteDep(item);
            }
            UpdateGridShow();
        }

        private void UpdateTable(object sender, RoutedEventArgs e)
        {
            UpdateGridShow();
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
            ApplicationsGrid.SelectAllCells();
            ApplicationsGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, ApplicationsGrid);
            ws.Paste();
            ws.Range["A1", "H1"].Font.Bold = true;
            int number1 = ws.UsedRange.Rows.Count;
            Microsoft.Office.Interop.Excel.Range myRange = ws.Range["A1", "H" + number1];
            myRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            myRange.WrapText = false;
            ws.Columns.EntireColumn.AutoFit();
        }

        private void DateTimeSearch(object sender, RoutedEventArgs e)
        {
            DateTime StartDate = DateTime.ParseExact($"{FirstDate.Text} 00:00:00,531", "yyyy.MM.dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact($"{SecondDate.Text} 00:00:00,531", "yyyy.MM.dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

            _dep = new List<ApplicationsDto>();
            _dep = _depManager.GetAllDepartments().ToList();

            List <ApplicationsDto> UpdatedGrid = new List<ApplicationsDto>();
            foreach (ApplicationsDto dto in _dep)
            {
                DateTime ActiveDate = DateTime.ParseExact($"{dto.Date} 00:00:00,531", "yyyy.MM.dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
                if (ActiveDate > StartDate && ActiveDate < EndDate)
                {
                    UpdatedGrid.Add(dto);
                }
            }
            ApplicationsGrid.ItemsSource = UpdatedGrid;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            LoginPage userSettingsPage = new LoginPage(_employeeContext, _mainWindow);
        }

        private void ShowUsersMenu(object sender, RoutedEventArgs e)
        {
            UsersListPage page = new UsersListPage(_mainWindow, _employeeContext);
        }

        private List<ApplicationsDto> _dep;
        private GeneralManager _depManager;
        private EmployeeContext _employeeContext;
        private Window _mainWindow;
        private List<Page> _pages;
    }
}