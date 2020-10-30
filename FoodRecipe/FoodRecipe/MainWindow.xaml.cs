using System;
using System.Collections.Generic;
using System.IO;
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

        List<Recipe> recipes;
        int currentPageIndex = 0;
        int itemPerPage = 8;
        int totalPage;

        public List<Recipe> GetAllRecipe(String pathRoot)
        {
            List <Recipe> recipes = new List<Recipe>();
            var recipeDirInfor = new DirectoryInfo($"{pathRoot}Data").GetDirectories();
            var recipeCount = recipeDirInfor.Length;

            for (int i = 0; i < recipeCount; i++)
            {
                Recipe tmp = new Recipe();
                tmp.GetFromFiles(pathRoot, $"{recipeDirInfor[i].Name}");
                recipes.Add(tmp);
            }
            return recipes;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            recipes = GetAllRecipe(pathRoot);
          
            int itemCount = recipes.Count();

            totalPage = itemCount / itemPerPage;
            if (itemCount % itemPerPage != 0)
            {
                totalPage += 1;
            }

            
            currentPageIndex = 1;
            dataListView.ItemsSource = recipes.Take(itemPerPage);
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
            this.Show();
        }
        private void Prv_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex <= totalPage )
            {
                currentPageIndex--;
                dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
                if (currentPageIndex <= 1)
                {
                    currentPageIndex = 1;
                }
            }
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = 1;
            dataListView.ItemsSource = recipes.Take(itemPerPage);
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = 2;
            dataListView.ItemsSource=recipes.Skip((currentPageIndex-1)*itemPerPage).Take(itemPerPage);
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex = 3;
            dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
        }

        private void Nxt_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex < totalPage)
            {
                currentPageIndex++;
                dataListView.ItemsSource = recipes.Skip((currentPageIndex - 1) * itemPerPage).Take(itemPerPage);
            }
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
