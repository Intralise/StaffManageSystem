﻿#pragma checksum "..\..\ApplicationsListPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5C8A58A1994E8395BBC68633EFFFAD98BAEF206ADA96D6A20E1ADD40D1C7BB8D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MyCourseWork;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MyCourseWork {
    
    
    /// <summary>
    /// ApplicationsListPage
    /// </summary>
    public partial class ApplicationsListPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\ApplicationsListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ApplicationsGrid;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\ApplicationsListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FirstDate;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\ApplicationsListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SecondDate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyCourseWork;component/applicationslistpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ApplicationsListPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 17 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowUsersMenu);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ApplicationsGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 22 "..\..\ApplicationsListPage.xaml"
            this.ApplicationsGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EmployeesGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 24 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateApplication);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 25 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteApplication);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 26 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AppClose);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 31 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateTable);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 32 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportToExcel);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 33 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LogOut);
            
            #line default
            #line hidden
            return;
            case 9:
            this.FirstDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.SecondDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            
            #line 36 "..\..\ApplicationsListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DateTimeSearch);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
