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
    public class Recipe
    {
        public String name { get; set; }
        public String ingredient { get; set; }
        public String thumbnailPath { get; set; }
        public bool isFavorite { get; set; }
        public List<List<String>> step { get; set; }
        public String youtubeLink { get; set; }
        public String heartShape { get; set; }
        public String heartColor { get; set; }
        //
        public Recipe(String newName, String newIngredient, String newThumbnailPath, String newYoutubeLink, bool newIsFavorite, List<List<String>> newStep, String newHeartShape, String newHeartColor)
        {
            name = newName;
            ingredient = newIngredient;
            thumbnailPath = newThumbnailPath;
            isFavorite = newIsFavorite;
            youtubeLink = newYoutubeLink;
            step = newStep;
            heartShape = newHeartShape;
            heartColor = newHeartColor;
        }
        public Recipe() 
        {
            name = "";
            ingredient = "";
            thumbnailPath = "";
            isFavorite = false;
            step = new List<List<string>>();
           
        }
        public bool SaveToFiles(String pathRoot)
        {
            bool result = true;
            try
            {
                String pathFood = $"{pathRoot}Data/{name}";
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
                    file.WriteLine($"{this.ingredient}");
                }
                for (int i=1; i<=this.step.Count(); i++)
                {
                    System.IO.Directory.CreateDirectory($"{pathFood}/{i}");
                    using (var file = new System.IO.StreamWriter($"{pathFood}/{i}/text.txt"))
                    {
                        file.WriteLine(this.step[i-1][0]);
                    }
                    
                    for (int j = 1; j < this.step[i-1].Count;j++)
                    {
                        var imageStep = new Bitmap($"{this.step[i-1][j]}");
                        imageStep.Save($"{pathFood}/{i}/{j}.jpg");
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
                    this.ingredient = tmp;

                    tmp = file.ReadLine();
                    this.heartShape = tmp;

                    tmp = file.ReadLine();
                    this.heartColor = tmp;
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
        public void UpdateRecipe(String newName, String newIngredient, String newThumbnailPath, String newYoutubeLink, bool newIsFavorite, List<List<String>> newStep)
        {
            name = newName;
            ingredient = newIngredient;
            thumbnailPath = newThumbnailPath;
            isFavorite = newIsFavorite;
            youtubeLink = newYoutubeLink;
            step = newStep;
        }
    }
}
