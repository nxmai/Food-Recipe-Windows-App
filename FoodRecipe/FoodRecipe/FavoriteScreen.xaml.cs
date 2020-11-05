using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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

        List<Recipe> favoriteRecipes;
        int currentPageIndex = 0;
        int itemPerPage = 8;
        int totalPage;

        public List<Recipe> GetAllRecipe(String pathRoot)
        {
            favoriteRecipes = new List<Recipe>();
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
            String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            favoriteRecipes = GetAllRecipe(pathRoot);


            int itemCount = favoriteRecipes.Count();

            totalPage = itemCount / itemPerPage;
            if (itemCount % itemPerPage != 0)
            {
                totalPage += 1;
            }


            currentPageIndex = 1;
            dataListView.ItemsSource = favoriteRecipes.Take(itemPerPage);
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
            currentPageIndex = 2;
            dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = 3;
            dataListView.ItemsSource = favoriteRecipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
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

        }

        private void HomeScreen_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow(); //window2 == homescreen
            this.Close();
            screen.ShowDialog();
        }
    }
}
