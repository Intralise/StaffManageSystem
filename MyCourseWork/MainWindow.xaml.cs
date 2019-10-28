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
using System.Data;
using System.Data.OleDb;
using MyCourseWork.AuthorizationService.Entities;
using MyCourseWork.AuthorizationService;
using AutoMapper;

namespace MyCourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _pages = new List<Page>();
            _employeeContext = new EmployeeContext();
            _employeeContext.Database.CreateIfNotExists();
            this.Content = new LoginPage(_employeeContext, this);
        }


        protected EmployeeManager _employeeManager;
        protected EmployeeContext _employeeContext;

        protected List<Page> _pages;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
