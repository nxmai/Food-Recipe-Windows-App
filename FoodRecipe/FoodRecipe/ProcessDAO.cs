using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;

namespace FoodRecipe
{
    class ProcessDAO
    {

        public static Entire_Process GetAll(string foodName)
        {
            var result = new Entire_Process();

            result.name = foodName;


            string folder = "";
            folder = AppDomain.CurrentDomain.BaseDirectory;
            folder += $"/data/{ foodName}";


            //doc so luong buoc cua mon an
            var amount_file = System.IO.File.ReadAllLines($"{folder}/amount.txt");
            int amount = 0;
            Int32.TryParse(amount_file[0], out amount);


            //lay bitmap cua avatar tu file
            var uri = new Uri($"{folder}/thumbnail.jpg", UriKind.Absolute);
            var bitmap = new BitmapImage(uri);
            result.avatar = bitmap;


            //lay data cua ingredient tu chuoi
            var ingredient_file = System.IO.File.ReadAllLines($"{folder}/description.txt");

            //lay link youtube tu file ingredient.txt
            result.link_youtube = ingredient_file[1];

            //lay danh sach cac thanh phan tu file
            result.ingredients = ingredient_file[2];


            //
            List<Step> tempListStep = new List<Step>();
            for (int i = 1; i <= amount; i++)
            {
                tempListStep.Add(GetStep(folder, i));
            }

            result.steps = tempListStep;

            return result;
        }

        public static Step GetStep(string folder, int numberStep)
        {
            var result = new Step();

            var amount_file = System.IO.File.ReadAllLines($"{folder}/{numberStep}/amount.txt");
            int amount;
            Int32.TryParse(amount_file[0], out amount);


            //doc du lieu tu file luu vao result.instruction
            var text_file = System.IO.File.ReadAllLines($"{folder}/{numberStep}/text.txt");
            List<string> temp_instruction = new List<string>();
            foreach (var line in text_file)
            {
                temp_instruction.Add(line);
            }

            //MessageBox.Show(temp_instruction[2]);
            result.instructions = temp_instruction;


            List<BitmapImage> temp_bitmap = new List<BitmapImage>();
            for (int i = 1; i <= amount; i++)
            {
                var uri = new Uri($"{folder}/{numberStep}/{i}.jpg", UriKind.Absolute);
                var bitmap = new BitmapImage(uri);
                //MessageBox.Show(bitmap.ToString());
                temp_bitmap.Add(bitmap);
            }
            result.images = temp_bitmap;

            return result;
        }
    }
}
