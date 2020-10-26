using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.IO;

namespace FoodRecipe
{
    class Recipe
    {
        private String name { get; set; }
        private String description { get; set; }
        private String thumbnailPath { get; set; }
        private bool isFavorite { get; set; }
        private List<List<String>> step { get; set; } 
        //link youtube
        //
        public Recipe(String newName, String newDescription, String newThumbnailPath, bool newIsFavorite, List<List<String>> newStep)
        {
            name = newName;
            description = newDescription;
            thumbnailPath = newThumbnailPath;
            isFavorite = newIsFavorite;
            step = newStep;
        }
        public bool SaveToFiles(String pathRoot)
        {
            bool result = true;
            try
            {
                String pathFood = $@"{pathRoot}/Recipe/{name}";
                String pathStep = $@"{pathFood}/Steps";
                System.IO.Directory.CreateDirectory(pathFood);
                System.IO.Directory.CreateDirectory(pathStep);
                var thumbnail = new Bitmap($@"{this.thumbnailPath}");
                thumbnail.Save($@"{pathFood}/thumbnail.png");
                using (var file = new System.IO.StreamWriter($@"{pathFood}/desription.txt"))
                {
                    file.WriteLine($"Description : {this.description}");
                    file.WriteLine($"Favorite : {this.isFavorite}");
                }
                for (int i=1; i<=this.step.Count(); i++)
                {
                    System.IO.Directory.CreateDirectory($@"{pathFood}/{i}");
                    using (var file = new System.IO.StreamWriter($@"{pathFood}/{i}/tutorial.txt"))
                    {
                        file.WriteLine(this.step[i-1][0]);
                    }
                    
                    for (int j = 1; j < this.step[i].Count;j++)
                    {
                        var imageStep = new Bitmap($@"{this.step[i][j]}");
                        imageStep.Save($@"{pathFood}/{i}/{j}.png");
                    }
                }
            }
            catch (Exception e)
            {
                using (var file = new System.IO.StreamWriter($"{pathRoot}/log.txt",true))
                {
                    file.WriteLine(e.Message);
                    Debug.WriteLine(e.Message);
                }
                result = false;
            }
            return result;
        }
        public bool GetFromFiles(String pathRoot, String pathFood)
        {
            bool result = false;
            try
            {
                this.thumbnailPath = $"{pathFood}/thumbnail.png";
                String pathStep = $"{pathFood}/Steps";
                var thumbnail = new BitmapImage(
                     new Uri(
                            $"{thumbnailPath}",
                            UriKind.RelativeOrAbsolute)
                    );

                using (var file = new System.IO.StreamReader($"{pathFood}/desription.txt"))
                {
                    var tmp = file.ReadLine();
                    var tokens = tmp.Split(new String[] { ":"}, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens[0] == "Description")
                    {
                        this.description = tokens[1];
                    }
                    file.ReadLine();
                    tokens = tmp.Split(new String[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens[0] == "Favorite")
                    {
                        if (tokens[1] == "1")
                            this.isFavorite = true;
                        else
                            this.isFavorite = false;
                    }

                    var stepCount = new DirectoryInfo($@"{pathFood}").GetDirectories().Length;
                    for (int i = 1; i <= stepCount; i++)
                    {
                        System.IO.Directory.CreateDirectory($@"{pathFood}/{i}");
                        using (var file1 = new System.IO.StreamReader($@"{pathFood}/{i}/tutorial.txt"))
                        {
                            this.step[i - 1][0] = file1.ReadToEnd();
                        }

                        var ImagesListObj = new DirectoryInfo($@"{pathFood}/{i}").GetFiles("*.png");
                        for (int j = 0; j < ImagesListObj.Length; j++)
                        {
                            step[i - 1][j] = ImagesListObj[j].DirectoryName;
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                using (var file = new System.IO.StreamWriter($"{pathRoot}/log.txt", true))
                {
                    file.WriteLine(e.Message);
                    Debug.WriteLine(e.Message);
                }
                result = false;
            }
            return result;
        }
    }
}
