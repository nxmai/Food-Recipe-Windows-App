﻿#pragma checksum "..\..\FavoriteScreen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B89288EE20BAA1AD74C5FCD88EC3A42EF985B969882B6C32D7D383EC1B137737"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.WPF;
using FontAwesome.WPF.Converters;
using FoodRecipe;
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


namespace FoodRecipe {
    
    
    /// <summary>
    /// FavoriteScreen
    /// </summary>
    public partial class FavoriteScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 28 "..\..\FavoriteScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView dataListView;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\FavoriteScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Prv;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\FavoriteScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Page1;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\FavoriteScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Page2;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\FavoriteScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Page3;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\FavoriteScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Nxt;
        
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
            System.Uri resourceLocater = new System.Uri("/FoodRecipe;component/favoritescreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FavoriteScreen.xaml"
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
            
            #line 9 "..\..\FavoriteScreen.xaml"
            ((FoodRecipe.FavoriteScreen)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dataListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.Prv = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\FavoriteScreen.xaml"
            this.Prv.Click += new System.Windows.RoutedEventHandler(this.Prv_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Page1 = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\FavoriteScreen.xaml"
            this.Page1.Click += new System.Windows.RoutedEventHandler(this.Page1_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Page2 = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\FavoriteScreen.xaml"
            this.Page2.Click += new System.Windows.RoutedEventHandler(this.Page2_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Page3 = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\FavoriteScreen.xaml"
            this.Page3.Click += new System.Windows.RoutedEventHandler(this.Page3_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Nxt = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\FavoriteScreen.xaml"
            this.Nxt.Click += new System.Windows.RoutedEventHandler(this.Nxt_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 107 "..\..\FavoriteScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HomeScreen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            
            #line 46 "..\..\FavoriteScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Favorite_Click);
            
            #line default
            #line hidden
            break;
            case 4:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseLeftButtonUpEvent;
            
            #line 59 "..\..\FavoriteScreen.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

