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
            String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            List<Recipe> recipes = GetAllRecipe(pathRoot);
            for (int i = 0; i < recipes.Count(); i++)
            {
                //MessageBox.Show($"{recipes[i].name}\n{recipes[i].ingredient}\n{recipes[i].thumbnailPath}");
            }
        }

        public List<Recipe> GetAllRecipe(String pathRoot)
        {
            List <Recipe> recipes = new List<Recipe>();
            var recipeDirInfor = new DirectoryInfo($"{pathRoot}Data").GetDirectories();
            var recipeCount = recipeDirInfor.Length;
            for (int i =0; i< recipeCount;i++)
            {
                Recipe tmp = new Recipe();
                tmp.GetFromFiles(pathRoot, $"{recipeDirInfor[i].Name}");
                recipes.Add(tmp);
            }
            return recipes;
        }

        private void addRecipe_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
