﻿#pragma checksum "..\..\RecordOrLoad.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0D3F49FE3AF0D5E211E4EEA2FE1BF56CA434E46C"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
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
using usicMusic;


namespace usicMusic {
    
    
    /// <summary>
    /// RecordOrLoad
    /// </summary>
    public partial class RecordOrLoad : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\RecordOrLoad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ApplicationBorder;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\RecordOrLoad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnExitBack;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\RecordOrLoad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnExit;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\RecordOrLoad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnRecord;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\RecordOrLoad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoad;
        
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
            System.Uri resourceLocater = new System.Uri("/usicMusic;component/recordorload.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RecordOrLoad.xaml"
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
            this.ApplicationBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.btnExitBack = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.btnExit = ((System.Windows.Controls.Image)(target));
            
            #line 22 "..\..\RecordOrLoad.xaml"
            this.btnExit.MouseEnter += new System.Windows.Input.MouseEventHandler(this.btnExit_MouseEnter);
            
            #line default
            #line hidden
            
            #line 22 "..\..\RecordOrLoad.xaml"
            this.btnExit.MouseLeave += new System.Windows.Input.MouseEventHandler(this.btnExit_MouseLeave);
            
            #line default
            #line hidden
            
            #line 22 "..\..\RecordOrLoad.xaml"
            this.btnExit.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.btnExit_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 22 "..\..\RecordOrLoad.xaml"
            this.btnExit.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.btnExit_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnRecord = ((System.Windows.Controls.Image)(target));
            
            #line 23 "..\..\RecordOrLoad.xaml"
            this.btnRecord.MouseEnter += new System.Windows.Input.MouseEventHandler(this.btnRecord_MouseEnter);
            
            #line default
            #line hidden
            
            #line 23 "..\..\RecordOrLoad.xaml"
            this.btnRecord.MouseLeave += new System.Windows.Input.MouseEventHandler(this.btnRecord_MouseLeave);
            
            #line default
            #line hidden
            
            #line 23 "..\..\RecordOrLoad.xaml"
            this.btnRecord.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.btnRecord_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 23 "..\..\RecordOrLoad.xaml"
            this.btnRecord.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.btnRecord_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnLoad = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\RecordOrLoad.xaml"
            this.btnLoad.Click += new System.Windows.RoutedEventHandler(this.btnLoad_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

