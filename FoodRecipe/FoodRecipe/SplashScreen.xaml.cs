using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            DisplaySplashScreen();
        }

        private void displayImage(string imgName1, string imgName2)
        {
            var bitmap = new BitmapImage(
                        new Uri($"SplashImages/{imgName1}",
                        UriKind.Relative)
                        );
            image1.Source = bitmap;

            bitmap = new BitmapImage(
                        new Uri($"SplashImages/{imgName2}",
                        UriKind.Relative)
                        );
            image2.Source = bitmap;
        }

        void DisplaySplashScreen ()
        {
            displayImage("soybean.jpg", "spinach.jpg");
            FoodName.Content = "Đậu nành và rau chân vịt";
            FoodContent.Text = "Một số người có thói quen ăn đậu nành kèm với rau chân vịt, nhưng các chuyên gia lại khuyến cáo rằng đây là một sự kết hợp nguy hiểm cho dạ dày của bạn. Rau chân vịt có chứa axit oxalic, khi kết hợp với canxi trong rau chân vịt sẽ tạo ra một loại kết tủa không tan gọi là canxi oxalat trong dạ dày của bạn.";
        }

        private void continue_click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            this.Close();
            screen.ShowDialog();
            
        }
    }
}
