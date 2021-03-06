﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool isSearching = false;

        BindingList<Recipe> recipes;
        int currentPageIndex = 1;
        int itemPerPage = 8;
        int totalPage;
        //String DefaultColor = "#f7ce38";
        //String oldColor;

        public BindingList<Recipe> search_Recipes = new BindingList<Recipe>();

        public BindingList<Recipe> GetAllRecipe(String pathRoot)
        {
            BindingList<Recipe> recipes = new BindingList<Recipe>();
            var recipeDirInfor = new DirectoryInfo($"{pathRoot}Data").GetDirectories();
            var recipeCount = recipeDirInfor.Length;

            for (int i = 0; i < recipeCount; i++)
            {
                Recipe tmp = new Recipe();
                tmp.GetFromFiles(pathRoot, $"{recipeDirInfor[i].Name}");
                recipes.Add(tmp);
            }
            recipes = new BindingList<Recipe>(recipes.OrderBy(x => x.DateCreate).ToList());

            //recipes = new BindingList<Recipe>(recipes.OrderByDescending(x => x.name).ToList());

            return recipes;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void load()
        {
            String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            recipes = GetAllRecipe(pathRoot);

            int itemCount = recipes.Count();

            totalPage = itemCount / itemPerPage;
            if (itemCount % itemPerPage != 0)
            {
                totalPage += 1;
            }

            dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);



            search_Recipes = recipes;
        }


        private void addRecipe_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeScreen = new AddRecipeWindow();
            addRecipeScreen.Dying += addRecipeScreenClosing;
            this.Hide();
            addRecipeScreen.Show();
        }

        private void addRecipeScreenClosing()
        {
            this.load();
            this.Show();
        }

        private void SettingScreenClosing()
        {
            this.IsEnabled = true;
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            bool showSplash = bool.Parse(value);
            
        }
        private void detailScreenClosing()
        {
            this.Show();
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = 1;
            if (isSearching == true)
            {
                dataListView.ItemsSource = searchInList(recipes).Take(itemPerPage);
            }
            else
            {
                dataListView.ItemsSource = recipes.Take(itemPerPage);
            }
        }

        private void Prv_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex <= totalPage)
            {
                if (isSearching == true)
                {
                    currentPageIndex--;
                    dataListView.ItemsSource = searchInList(recipes).Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
                    if (currentPageIndex <= 1)
                    {
                        currentPageIndex = 1;
                    }
                }
                else
                {
                    currentPageIndex--;
                    dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
                    if (currentPageIndex <= 1)
                    {
                        currentPageIndex = 1;
                    }
                }
            }
        }

        

        private void Nxt_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex < totalPage)
            {
                if (isSearching == true)
                {
                    currentPageIndex++;
                    dataListView.ItemsSource = searchInList(recipes).Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
                }
                else
                {
                    currentPageIndex++;
                    dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
                }
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = totalPage;
            if (isSearching == true)
            {
               
                dataListView.ItemsSource = searchInList(recipes).Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
            else
            {
                dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;


            if ((item as Recipe).heartShape == "Heart" && (item as Recipe).heartColor == "Red")
            {
                (item as Recipe).heartShape = "HeartOutline";
                (item as Recipe).heartColor = "White";

                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}Data/{(item as Recipe).name}/description.txt";
                var lines = File.ReadAllLines(database);

                lines[0] = "false";
                lines[3] = "HeartOutline";
                lines[4] = "White";

                File.WriteAllLines(database, lines);
            }
            else
            {
                (item as Recipe).heartShape = "Heart";
                (item as Recipe).heartColor = "Red";


                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}Data/{(item as Recipe).name}/description.txt";
                var lines = File.ReadAllLines(database);

                lines[0] = "true";
                lines[3] = "Heart";
                lines[4] = "Red";

                File.WriteAllLines(database, lines);


            }
        }

        private void toFavoriteScreen_Click(object sender, RoutedEventArgs e)
        {
            var screen = new FavoriteScreen();
            this.Close();
            screen.ShowDialog();
        }

        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }



        private void search_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if (LocDau(recipes[i].name).ToUpper().Contains(LocDau(searchTextBox.Text.ToUpper())))
                    MessageBox.Show(LocDau(recipes[i].name));
            }
        }


        public BindingList<Recipe> searchInList(BindingList<Recipe> search_recipe)
        {
            BindingList<Recipe> result = new BindingList<Recipe>();

            for (int i = 0; i < recipes.Count; i++)
            {
                if (LocDau(recipes[i].name).ToUpper().Contains(LocDau(searchTextBox.Text.ToUpper())))
                    result.Add(search_Recipes[i]);
            }



            return result;
        }



        private void search_Press(object sender, KeyEventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                isSearching = false;
            }
            else
            {
                isSearching = true;
            }
            // dataListView.ItemsSource = searchInList(recipes).Take(itemPerPage);
            dataListView.ItemsSource = searchInList(recipes).Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);

        }

        private void leave_Search_Event(object sender, DragEventArgs e)
        {

        }

        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //search_Recipes = recipes;
            //searchTextBox.Text = "";
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            //var temp = sender as Button;

            //if (temp != null)
            //{
            //    MessageBox.Show("hi");
            //}
            //else
            //{
            //    var item = (sender as FrameworkElement).DataContext;
            //    //messagebox.show((item as recipe).name);

            //    var detailscreen = new DetailScreen((item as Recipe).name);

            //    this.Hide();

            //    detailscreen.Dying += detailScreenClosing;

            //    detailscreen.Show();
            //}

            var item = (sender as FrameworkElement).DataContext;
            //messagebox.show((item as recipe).name);

            var detailscreen = new DetailScreen((item as Recipe).name);

            this.Hide();

            detailscreen.Dying += detailScreenClosing;

            detailscreen.Show();

        }

        private void AscSort_Click(object sender, RoutedEventArgs e)
        {
            recipes = new BindingList<Recipe>(recipes.OrderBy(x => x.name).ToList());

            dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void DesSort_CLick(object sender, RoutedEventArgs e)
        {
            recipes = new BindingList<Recipe>(recipes.OrderByDescending(x => x.name).ToList());

            dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void AscDateSort_Click(object sender, RoutedEventArgs e)
        {
            recipes = new BindingList<Recipe>(recipes.OrderBy(x => x.DateCreate).ToList());

            dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void AscDesSort_Click(object sender, RoutedEventArgs e)
        {
            recipes = new BindingList<Recipe>(recipes.OrderByDescending(x => x.DateCreate).ToList());

            dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            var main = this;
            var settingScreen = new SettingScreen();
            settingScreen.Show();
            settingScreen.Topmost = true;
            settingScreen.Focus();
            this.IsEnabled = false;
            settingScreen.Dying += SettingScreenClosing;
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Window2();
            this.Close();
            screen.ShowDialog();
        }
    }

}