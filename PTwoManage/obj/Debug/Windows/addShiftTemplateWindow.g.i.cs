﻿#pragma checksum "..\..\..\Windows\addShiftTemplateWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0D50E195543386CED33925F5FE8ABAD4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace PTwoManage {
    
    
    /// <summary>
    /// AddShiftTemplateWindow
    /// </summary>
    public partial class AddShiftTemplateWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Day_List;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Start_Time;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox End_Time;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Tag_List;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tag_Add_TextBox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Shift_Template_List;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Windows\addShiftTemplateWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Error_message;
        
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
            System.Uri resourceLocater = new System.Uri("/P2Manage;component/windows/addshifttemplatewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\addShiftTemplateWindow.xaml"
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
            this.Day_List = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.Start_Time = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\Windows\addShiftTemplateWindow.xaml"
            this.Start_Time.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.EditTime_NumberValidation);
            
            #line default
            #line hidden
            return;
            case 3:
            this.End_Time = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\Windows\addShiftTemplateWindow.xaml"
            this.End_Time.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.EditTime_NumberValidation);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 23 "..\..\..\Windows\addShiftTemplateWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_ShiftTemplate);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Tag_List = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.Tag_Add_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 27 "..\..\..\Windows\addShiftTemplateWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_Tag_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 28 "..\..\..\Windows\addShiftTemplateWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Tag_Delete_From_Listbox_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Shift_Template_List = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.Error_message = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            
            #line 50 "..\..\..\Windows\addShiftTemplateWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
