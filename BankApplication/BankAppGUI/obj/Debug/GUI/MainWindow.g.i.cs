﻿#pragma checksum "..\..\..\GUI\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "60CC9C86E1A0B45BFB8457CB80FC3B037622A3C91A5A68CC9A02D3B080BBED67"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BankAppGUI;
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


namespace BankAppGUI {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockBankName;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_CurrentDate;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_name;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_type;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_value;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_daysLeft_Title;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_daysLeft;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\GUI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel buttonsManagersAccount;
        
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
            System.Uri resourceLocater = new System.Uri("/BankAppGUI;component/gui/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GUI\MainWindow.xaml"
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
            this.textBlockBankName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.textBlock_CurrentDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 44 "..\..\..\GUI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SetDAte);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 54 "..\..\..\GUI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuButton_OpenAccount);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 62 "..\..\..\GUI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuButton_FindAccountAtId);
            
            #line default
            #line hidden
            return;
            case 6:
            this.textBlock_name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.textBlock_type = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.textBlock_value = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.textBlock_daysLeft_Title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.textBlock_daysLeft = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.buttonsManagersAccount = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 12:
            
            #line 120 "..\..\..\GUI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AccountMenu_Put);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 123 "..\..\..\GUI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AccountMenu_Withdraw);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 126 "..\..\..\GUI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AccountMenu_CloseAccount);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
