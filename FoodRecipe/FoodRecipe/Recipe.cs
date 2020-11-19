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
using System.Security.Principal;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FoodRecipe
{
    public class Recipe : INotifyPropertyChanged
    {
        public String name { get; set; }
        public String ingredient { get; set; }
        public String thumbnailPath { get; set; }
        public bool isFavorite { get; set; }
        public List<List<String>> step { get; set; }
        public String youtubeLink { get; set; }
        public String heartShape { get; set; }
        public String heartColor { get; set; }
        public DateTime DateCreate { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        //

        public Recipe(String newName, String newIngredient, String newThumbnailPath, String newYoutubeLink, bool newIsFavorite, List<List<String>> newStep, String newHeartShape, String newHeartColor)
        {
            name = newName;
            ingredient = newIngredient;
            thumbnailPath = newThumbnailPath;
            isFavorite = newIsFavorite;
            if (newYoutubeLink == "Link Youtbe")
                youtubeLink = "";
            else
                youtubeLink = newYoutubeLink;
            step = newStep;
            heartShape = newHeartShape;
            heartColor = newHeartColor;
            DateCreate = DateTime.Now;
        }

        public Recipe()
        {
            name = "";
            ingredient = "";
            thumbnailPath = "";
            isFavorite = false;
            step = new List<List<string>>();

        }

        public int SaveToFiles(String pathRoot)
        {
            int result = 0;
            try
            {
                String pathFood = $"{pathRoot}Data/{name}";
                if (new DirectoryInfo($"{pathFood}").Exists)
                {
                    result = 1;
                }
                else
                {
                    System.IO.Directory.CreateDirectory(pathFood);
                    var thumbnail = new Bitmap($"{this.thumbnailPath}");
                    thumbnail.Save($"{pathFood}/thumbnail.jpg");
                    using (var file = new System.IO.StreamWriter($"{pathFood}/description.txt"))
                    {
                        /*file.WriteLine($"Favorite : {this.isFavorite}");
                        file.WriteLine($"Youtube : {this.youtubeLink}");
                        file.WriteLine($"Ingredient : {this.ingredient}");*/
                        file.WriteLine($"{this.isFavorite}");
                        file.WriteLine($"{this.youtubeLink}");
                        this.ingredient = this.ingredient.Replace('\n',' ');
                        this.ingredient = this.ingredient.Replace('\r', ',');
                        file.WriteLine($"{this.ingredient}");
                        file.WriteLine($"{this.heartShape}");
                        file.WriteLine($"{this.heartColor}");
                        file.WriteLine($"{this.DateCreate}");
                    }
                    for (int i = 1; i <= this.step.Count(); i++)
                    {
                        System.IO.Directory.CreateDirectory($"{pathFood}/{i}");
                        using (var file = new System.IO.StreamWriter($"{pathFood}/{i}/amount.txt"))
                        {
                            file.WriteLine((step[i - 1].Count() - 1).ToString());
                        }
                        using (var file = new System.IO.StreamWriter($"{pathFood}/{i}/text.txt"))
                        {
                            file.WriteLine(this.step[i - 1][0]);
                        }

                        for (int j = 1; j < this.step[i - 1].Count; j++)
                        {
                            var imageStep = new Bitmap($"{this.step[i - 1][j]}");
                            imageStep.Save($"{pathFood}/{i}/{j}.jpg");
                        }
                    }
                    using (var file = new System.IO.StreamWriter($"{pathFood}/amount.txt"))
                    {
                        file.WriteLine(step.Count().ToString());
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
                result = -1;
            }
            return result;
        }

        public bool GetFromFiles(String pathRoot, String foodName)
        {
            bool result = false;
            try
            {
                this.name = foodName;
                String pathFood = $"{pathRoot}Data/{foodName}";
                this.thumbnailPath = $"{pathFood}/thumbnail.jpg";
                var thumbnail = new BitmapImage(
                     new Uri(
                            $"{thumbnailPath}",
                            UriKind.Absolute)
                    );

                using (var file = new System.IO.StreamReader($"{pathFood}/description.txt"))
                {
                    var tmp = file.ReadLine();
                    if (tmp == "true")
                        this.isFavorite = true;
                    if (tmp == "false")
                        this.isFavorite = false;

                    tmp = file.ReadLine();
                    this.youtubeLink = tmp;

                    tmp = file.ReadLine();
                    tmp = tmp.Replace(',', '\n');
                    this.ingredient = tmp;

                    tmp = file.ReadLine();
                    this.heartShape = tmp;

                    tmp = file.ReadLine();
                    this.heartColor = tmp;

                    tmp = file.ReadLine();
                    this.DateCreate = DateTime.Parse(tmp);
                }

                var stepCount = new DirectoryInfo($"{pathFood}").GetDirectories().Length;
                for (int i = 1; i <= stepCount; i++)
                {
                    List<String> tmpStep = new List<string>();
                    System.IO.Directory.CreateDirectory($"{pathFood}/{i}");
                    using (var file1 = new System.IO.StreamReader($"{pathFood}/{i}/text.txt"))
                    {
                        tmpStep.Add(file1.ReadToEnd());
                    }

                    var ImagesListObj = new DirectoryInfo($"{pathFood}/{i}").GetFiles("*.jpg");
                    for (int j = 0; j < ImagesListObj.Length; j++)
                    {
                        tmpStep.Add(ImagesListObj[j].DirectoryName);
                    }
                    this.step.Add(tmpStep);
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

        /*
      public bool GetFromFiles1(String pathRoot, String foodName)
        {
            bool result = false;
            try
            {
                String pathFood = $"{pathRoot}Data/{foodName}";
                /* var thumbnail = new BitmapImage(
                      new Uri(
                             $"{thumbnailPath}",
                             UriKind.Absolute)
                     );
                 using (var file = new System.IO.StreamReader($"{pathFood}/ingredient.txt"))
                 {
                     var tmp = file.ReadLine();
                     this.youtubeLink = tmp;
                     tmp = file.ReadToEnd();
                     tmp = tmp.Replace(',', '\n');
                     this.ingredient = tmp;
                 }
                 using (var file = new System.IO.StreamWriter($"{pathFood}/description.txt"))
                 {
                     file.WriteLine("false");
                     file.WriteLine($"{this.youtubeLink}");
                     this.ingredient = this.ingredient.Replace('\n', ' ');
                     this.ingredient = this.ingredient.Replace('\r', ',');
                     file.WriteLine($"{this.ingredient}");
                     file.WriteLine("HeartOutline");
                     file.WriteLine("White");
                 }
                
               
                var stepCount = new DirectoryInfo($"{pathFood}").GetDirectories().Length;
                for (int i = 1; i <= stepCount; i++)
                {
                    List<String> tmpStep = new List<string>();
                    using (var file1 = new System.IO.StreamReader($"{pathFood}/{i}/text.txt"))
                    {
                        tmpStep.Add(file1.ReadToEnd());
                    }
                    var ImagesListObj = new DirectoryInfo($"{pathFood}/{i}").GetFiles("*.jpg");
                    for (int j = 0; j < ImagesListObj.Length; j++)
                    {
                        tmpStep.Add(ImagesListObj[j].DirectoryName);
                    }
                    this.step.Add(tmpStep);
                }
                using (var file = new System.IO.StreamWriter($"{pathFood}/amount.txt"))
                {
                    file.WriteLine(step.Count().ToString());
                }
                for (int i = 1; i <= this.step.Count(); i++)
                {
                    using (var file = new System.IO.StreamWriter($"{pathFood}/{i}/amount.txt"))
                    {
                        file.WriteLine((step[i - 1].Count() - 1).ToString());
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
        }*/
    }
}