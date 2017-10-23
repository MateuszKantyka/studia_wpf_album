using Biblioteka_Multimedialna.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteka_Multimedialna.Controller
{
   public  class Inicjalization
    {
        public static void init()
        {
            var category1 = new Category
            {
                Name = "Natura"
            };

            var category2 = new Category
            {
                Name = "Auta"
            };

            var category3 = new Category
            {
                Name = "Wakacje"
            };

            var photo1 = new Picture
            {
                Description = "Skoro roślinkom...",
                Stars = 5,
                Image = "C:/Biblioteka Multimedialna/foto1.bmp",
                Category = category1,
            };

            var photo2 = new Picture
            {
                Description = "Skoro bleble...",
                Stars = 5,
                Image = "C:/Biblioteka Multimedialna/foto2.bmp",
                Category = category2,
            };

            var photo3 = new Picture
            {
                Description = "foto 3 bleble...",
                Stars = 4,
                Image = "C:/Biblioteka Multimedialna/foto3.bmp",
                Category = category3,
            };

            var photo4 = new Picture
            {
                Description = "foto 4 bleble...",
                Stars = 3,
                Image = "C:/Biblioteka Multimedialna/foto4.bmp",
                Category = category3,
            };

            var tag1 = new Tag { Name = "#Uśmiech" };
            var tag2 = new Tag { Name = "#Zielono" };
            var tag3 = new Tag { Name = "#Lato" };
            var tag4 = new Tag { Name = "#LubięTo" };

            var pictureTag1 = new PictureTag
            {
                Picture = photo1,
                Tag = tag1
            };

            var pictureTag2 = new PictureTag
            {
                Picture = photo1,
                Tag = tag2
            };

            var pictureTag3 = new PictureTag
            {
                Picture = photo2,
                Tag = tag1
            };

            var pictureTag4 = new PictureTag
            {
                Picture = photo4,
                Tag = tag2
            };

            var pictureTag5 = new PictureTag
            {
                Picture = photo3,
                Tag = tag1
            };

            using (var context = new Context())
            {
                context.PictureTags.Add(pictureTag1);
                context.PictureTags.Add(pictureTag2); 
                context.PictureTags.Add(pictureTag3);
                context.PictureTags.Add(pictureTag4);
                context.PictureTags.Add(pictureTag5);

                context.SaveChanges();
            }

            MessageBox.Show("Koniec!");
        }
    }
}
