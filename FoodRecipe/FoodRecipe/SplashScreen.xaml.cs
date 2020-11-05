using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool showSplash = true;
        public Window1()
        {
            InitializeComponent();

        }

        static Random _rng = new Random();
        private void DisplayImage(Tuple<string, string, string, string> images)
        {
            String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            var bitmap = new BitmapImage(
                        new Uri($"{pathRoot}SplashImages/{images.Item1}",
                        UriKind.Absolute)
                        );
            image1.Source = bitmap;

            bitmap = new BitmapImage(
                        new Uri($"{pathRoot}SplashImages/{images.Item2}",
                        UriKind.Absolute)
                        );
            image2.Source = bitmap;
        }

        void DisplaySplashScreen()
        {
            Tuple<string, string, string, string>[] imagesArray =
            {
                Tuple.Create("soy-bean.jpg", "spinach.jpg", "ĐẬU NÀNH VÀ RAU CHÂN VỊT", "Một số người có thói quen ăn đậu nành kèm với rau chân vịt, nhưng các chuyên gia lại khuyến cáo rằng đây là một sự kết hợp nguy hiểm cho dạ dày của bạn. Rau chân vịt có chứa axit oxalic, khi kết hợp với canxi trong rau chân vịt sẽ tạo ra một loại kết tủa không tan gọi là canxi oxalat trong dạ dày của bạn."),
                Tuple.Create("soy-milk.jpg", "egg.jpg", "SỮA ĐẬU NÀNH VÀ TRỨNG", "Trứng là một trong những nguồn cung cấp protein lớn nhất cho bạn. Tuy nhiên, khác với các loại sữa thông thường, sữa đậu nành có khả năng ức chế hoạt động của enzyme protease. Enzyme này có nhiệm vụ thúc đẩy quá trình trao đổi chất trong cơ thể của bạn. Chính vì thế, những thực phẩm kỵ nhau này sẽ làm mất đi những tác dụng có lợi của chúng. Sữa đậu nành sẽ làm cơ thể bạn không hấp thụ được toàn bộ số protein của trứng."),
                Tuple.Create("milk.jpg", "chocolate.jpg", "SỮA VÀ CHOCOLATE", "Mặc dù sự kết hợp này tạo ra một món ăn vô cùng ngon miệng, song các bác sĩ không khuyến khích việc kết hợp sữa và chocolate. Nguyên nhân là do sữa rất giàu canxi và protein, trong khi đó chocolate lại chứa nhiều axit oxalic. Nếu bạn kết hợp hai món ăn này tại cùng thời điểm, canxi trong sữa và axit oxalic từ chocolate có thể tạo ra canxi oxalat. Hợp chất này không những không thể hòa tan mà còn có thể dẫn đến chứng tiêu chảy."),
                Tuple.Create("meat.jpg","watermelon.jpg", "DƯA HẤU VÀ THỊT", "Thịt thường được xếp vào danh sách những thực phẩm “nóng” đối với cơ thể bạn và ngược lại, dưa hấu thuộc nhóm thực phẩm “mát”. Chính vì sự trái ngược này mà khi được kết hợp với nhau, mức độ hiệu quả về mặt dinh dưỡng của thịt sẽ bị giảm xuống trầm trọng. Không chỉ dừng lại ở đó, điều này thậm chí còn có thể gây nguy hiểm cho những người mắc chứng bệnh suy nhược lá lách và gây ảnh hưởng không nhỏ đến dạ dày của bạn."),
                Tuple.Create("shrimp.jpg", "vitaminc.jpg", "TÔM VÀ VITAMIN C", "Trong tôm thông thường có nhiều arsenic trioxide (As205). Chính vì thế, nếu bạn kết hợp tôm với các thực phẩm chức năng bổ sung vitamin C thì sẽ dẫn đến việc tạo ra những phản ứng hóa học trong dạ dày của bạn hình thành nên arsenic trioxide. Đây là những thực phẩm kỵ nhau có thể dẫn đến những vấn đề sức khỏe nghiêm trọng, thậm chí là tử vong."),
                Tuple.Create("persimmon.jpg", "potato.jpg", "HỒNG VÀ KHOAI TÂY", "Trong khoai tây có rất nhiều axit vô cơ. Việc kết hợp khoai và hồng có thể khiến cho cơ thể, cụ thể là dạ dày bạn chứa đầy cặn và xác của trái hồng. Những loại cặn này hầu như không thể hòa tan hoặc pha loãng, chính vì vậy chúng sẽ có ảnh hưởng không nhỏ đối với quá trình tiêu hóa của cơ thể bạn."),
            };

            int randomNumber = _rng.Next(6);

            DisplayImage(imagesArray[randomNumber]);
            FoodName.Text = imagesArray[randomNumber].Item3;
            FoodContent.Text = imagesArray[randomNumber].Item4;
        }

        private void continue_click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow(); //window2 == homescreen
            this.Close();
            screen.ShowDialog();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            showSplash = bool.Parse(value);
            Debug.WriteLine(value);
            if (showSplash == false)
            {
                DisplaySplashScreen();
                var screen = new MainWindow(); //window2 == homescreen
                this.Close();
                screen.ShowDialog();
            }
            else
            {
                DisplaySplashScreen();
            }

        }
    }
}
