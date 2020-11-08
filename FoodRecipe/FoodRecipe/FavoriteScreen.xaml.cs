using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for FavoriteScreen.xaml
    /// </summary>
    public partial class FavoriteScreen : Window
    {
        public FavoriteScreen()
        {
            InitializeComponent();
        }

        BindingList<Recipe> favoriteRecipes;
        int currentPageIndex = 1;
        int itemPerPage = 8;
        int totalPage;

        public BindingList<Recipe> GetAllRecipe(String pathRoot)
        {
            favoriteRecipes = new BindingList<Recipe>();
            var recipeDirInfor = new DirectoryInfo($"{pathRoot}Data").GetDirectories();
            var recipeCount = recipeDirInfor.Length;
            for (int i = 0; i < recipeCount; i++)
            {
                Recipe tmp = new Recipe();
                tmp.GetFromFiles(pathRoot, $"{recipeDirInfor[i].Name}");

                if (tmp.isFavorite == true)
                {
                    favoriteRecipes.Add(tmp);
                }


            }
            return favoriteRecipes;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loader();
        }

        private void loader()
        {
            String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            favoriteRecipes = GetAllRecipe(pathRoot);


            int itemCount = favoriteRecipes.Count();

            totalPage = itemCount / itemPerPage;
            if (itemCount % itemPerPage != 0)
            {
                totalPage += 1;
            }


            dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void Prv_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex <= totalPage)
            {
                currentPageIndex--;
                dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
                if (currentPageIndex <= 1)
                {
                    currentPageIndex = 1;
                }
            }
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = 1;
            dataListView.ItemsSource = favoriteRecipes.Take(itemPerPage);
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            if (totalPage > 1)
            {
                currentPageIndex = 2;
                dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            if (totalPage > 3)
            {
                currentPageIndex = 3;
                dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
        }

        private void Nxt_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex < totalPage)
            {
                currentPageIndex++;
                dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListView.Items.IndexOf(item) + ((currentPageIndex - 1) * itemPerPage);


            favoriteRecipes[index].heartShape = "HeartOutline";
            favoriteRecipes[index].heartColor = "White";

            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}Data/{favoriteRecipes[index].name}/description.txt";
            var lines = File.ReadAllLines(database);

            lines[0] = "false";
            lines[3] = "HeartOutline";
            lines[4] = "White";

            File.WriteAllLines(database, lines);


            loader();
        }

        private void HomeScreen_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow(); //window2 == homescreen
            this.Close();
            screen.ShowDialog();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;

            var detailScreen = new DetailScreen((item as Recipe).name);

            detailScreen.Show();
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
            this.loader();
            this.Show();
        }

    }
}