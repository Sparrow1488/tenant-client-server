﻿#pragma checksum "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AA03F7E8E270E6774E013E95B9E86F993F698364B1C870BDFECC4FFE94918FC8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using WpfApp1.Pages.HomePages.ChildLetterPage;


namespace WpfApp1.Pages.HomePages.ChildLetterPage {
    
    
    /// <summary>
    /// UniversalLetterPage
    /// </summary>
    public partial class UniversalLetterPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox titleBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton complaintType;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton offerType;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton questionType;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendBtn;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sourceAttacherBtn;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox sourceAtteched;
        
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
            System.Uri resourceLocater = new System.Uri("/MVVM-Pattern-Test;component/pages/homepages/childletterpage/universalletterpage." +
                    "xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
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
            this.titleBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.descBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.complaintType = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.offerType = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.questionType = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.sendBtn = ((System.Windows.Controls.Button)(target));
            
            #line 125 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
            this.sendBtn.Click += new System.Windows.RoutedEventHandler(this.SendLetterBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.sourceAttacherBtn = ((System.Windows.Controls.Button)(target));
            
            #line 141 "..\..\..\..\..\Pages\HomePages\ChildLetterPage\UniversalLetterPage.xaml"
            this.sourceAttacherBtn.Click += new System.Windows.RoutedEventHandler(this.AttachFile_Click_1);
            
            #line default
            #line hidden
            return;
            case 8:
            this.sourceAtteched = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

