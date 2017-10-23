using Biblioteka_Multimedialna.Controller;
using Biblioteka_Multimedialna.model;
using Biblioteka_Multimedialna.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Biblioteka_Multimedialna
{
    public partial class MainWindow : Window
    {
        private List<Picture> pictureToShow;
        private int index;

        public MainWindow()
        {
            //Inicjalization.init();
            //Directory.CreateDirectory("c:/Biblioteka Multimedialna");
            //i command
            //
            InitializeComponent();
            hideElement();
            pictureToShow = new List<Picture>();
            index = 0;
            List<Stars> starList = new List<Stars>
        {
            new Stars(){ Name = "*", Value=1 },
            new Stars(){ Name = "**", Value=2 },
            new Stars(){ Name = "***", Value=3 },
            new Stars(){ Name = "****", Value=4 },
            new Stars(){ Name = "*****", Value=5 }
        };

            starsListOfCheckBox.DataContext = starList;
            reloadPage();
        }

        private void BtnDeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                var picture = pictureToShow[index].PictureID;
                var querryForPicture = context.Pictures
                    .Where(x => x.PictureID == picture)
                    .Select(x => x).ToList();
                context.Pictures.Remove(querryForPicture.FirstOrDefault());
                context.SaveChanges();
            }
            hideElement();
            resetValue();
            enableClick();
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            resetValue();
            showElement();
            disableClick();
            starsListOfCheckBox.SelectionMode = SelectionMode.Single;
            categoryListOfCheckBox.SelectionMode = SelectionMode.Single;
            LbCategory.Visibility = Visibility.Hidden;
            BtnSavePhoto.Visibility = Visibility.Visible;
        }

        private void BtnSavePhoto_Click(object sender, RoutedEventArgs e)
        {
            var stars = (Stars)starsListOfCheckBox.SelectedItem;
            var category = (Category)categoryListOfCheckBox.SelectedItem;
            Tag tag;
            using (var context = new Context())
            {
                if (TbDescription.Text != null && TbLocation.Text != "" && stars != null && category != null && tagsListOfCheckBox.SelectedItems != null)
                {
                var photo = new Picture
                {
                    Description = TbDescription.Text,
                    Stars = stars.Value,
                    Image = TbLocation.Text,
                    CategoryID = category.CategoryID,
                };

                foreach (var item in tagsListOfCheckBox.SelectedItems)
                {
                    tag = (Tag)item;
                    var pictureTag = new PictureTag
                    {
                        Picture = photo,
                        TagID = tag.TagID
                    };
                    context.PictureTags.Add(pictureTag);
                }
                context.SaveChanges();
                starsListOfCheckBox.SelectionMode = SelectionMode.Multiple;
                categoryListOfCheckBox.SelectionMode = SelectionMode.Multiple;
                resetValue();
                hideElement();
                enableClick();
                }

                else
                {
                    MessageBox.Show("Prosze uzupełnić wszystkie pola!");
                }

            }
        }

        private void BtnEditPhoto_Click(object sender, RoutedEventArgs e)
        {
            var stars = (Stars)starsListOfCheckBox.SelectedItem;
            var category = (Category)categoryListOfCheckBox.SelectedItem;
            Tag tag;
            if (TbDescription.Text != null && TbLocation.Text != "" && stars != null && category != null && tagsListOfCheckBox.SelectedItems != null)
            {

            using (var context = new Context())
            {
                var picture = pictureToShow[index].PictureID;
                var querryForPicture = context.Pictures
                    .Where(x => x.PictureID == picture)
                    .Select(x => x).ToList();

                var querrtForTags = context.PictureTags
                    .Where(x => x.PictureID == picture)
                    .Select(x => x).ToList();

                foreach (var item in querrtForTags)
                {
                    context.PictureTags.Remove(item);
                }

                foreach (var item in tagsListOfCheckBox.SelectedItems)
                {
                    tag = (Tag)item;
                    var pictureTag = new PictureTag
                    {
                        PictureID = picture,
                        TagID = tag.TagID
                    };
                    context.PictureTags.Add(pictureTag);
                }

                querryForPicture.FirstOrDefault().Description = TbDescription.Text;
                querryForPicture.FirstOrDefault().Image = TbLocation.Text;
                querryForPicture.FirstOrDefault().Stars = stars.Value;
                querryForPicture.FirstOrDefault().CategoryID = category.CategoryID;
                context.SaveChanges();
            }

            hideElement();
            resetValue();
            enableClick();
            }
            else
            {
                MessageBox.Show("Prosze uzupełnić wszystkie pola!");
            }
        }

        private void BtnSearchPhoto_Click(object sender, RoutedEventArgs e)
        {
            pictureToShow.Clear();
            index = 0;
            BtnEditEnable.Visibility = Visibility.Visible;
            //resetValue();
            TbCategory.Text = "";
            TbDescription.Text = "";
            TbLocation.Text = "";
            TbTags.Text = "";
            TbStar.Text = "";
            ImgPhoto.Source = null;

            showElement();
            BtnPrevious.Visibility = Visibility.Visible;
            BtnNext.Visibility = Visibility.Visible;
            BtnDeletePhoto.Visibility = Visibility.Visible;
            categoryListOfCheckBox.IsEnabled = false;
            tagsListOfCheckBox.IsEnabled = false;
            starsListOfCheckBox.IsEnabled = false;
            
            disableClick();

            List<int> stars = new List<int>();

            foreach (var item in starsListOfCheckBox.SelectedItems)
            {
                var x = (Stars)item;
                stars.Add(x.Value);
            }

            bool isEmptyStar = !stars.Any();
            if (isEmptyStar)
            {
                for (int i = 1; i < 6; i++)
                {
                    stars.Add(i);
                }
            }

            List<int> categorys = new List<int>();

            foreach (var item in categoryListOfCheckBox.SelectedItems)
            {
                var x = (Category)item;
                categorys.Add(x.CategoryID);
            }

            bool isEmptyCategorys = !categorys.Any();
            if (isEmptyCategorys)
            {
                using (var context = new Context())
                {
                    categorys = context.Categorys.Select(x => x.CategoryID).ToList();
                }
            }

            List<int> tags = new List<int>();

            foreach (var item in tagsListOfCheckBox.SelectedItems)
            {
                var x = (Tag)item;
                tags.Add(x.TagID);
            }

            bool isEmptyTags = !tags.Any();
            if (isEmptyTags)
            {
                using (var context = new Context())
                {
                    tags = context.Tags.Select(x => x.TagID).ToList();
                }
            }

            using (var context = new Context())
            {
                var pictures = context.Pictures
                        .Where(x => stars.Contains(x.Stars))
                        .Where(x => categorys.Contains(x.CategoryID))
                        .Select(x => x.PictureID).ToList();

                List<int> pictureToShowID = new List<int>();

                foreach (var item in context.PictureTags)
                {
                    if (pictures.Contains(item.PictureID) && tags.Contains(item.TagID))
                    {
                        pictureToShowID.Add(item.PictureID);
                    }
                }

                pictureToShowID.Distinct();
                pictureToShow = context.Pictures
                    .Where(x => pictureToShowID.Contains(x.PictureID))
                    .Select(x => x).ToList();

                if (pictureToShow.Count > 0)
                {
                    showPicture();
                }

                else
                {
                    MessageBox.Show("Nie znaleziono zdjęć");
                    hideElement();
                    resetValue();
                    enableClick();
                }
            }
        }

        public void showPicture()
        {
            BtnNext.Visibility = Visibility.Visible;
            BtnPrevious.Visibility = Visibility.Visible;

            if (pictureToShow.Count() - 1 == index)
            {
                BtnNext.Visibility = Visibility.Hidden;
            }

            if (index == 0)
            {
                BtnPrevious.Visibility = Visibility.Hidden;
            }

            TbTags.Text = "";
            TbLocation.Text = pictureToShow[index].Image;
            ImgPhoto.Source = new BitmapImage(new Uri(pictureToShow[index].Image)); ;
            TbDescription.Text = pictureToShow[index].Description;
            TbStar.Text = pictureToShow[index].Stars.ToString();

            int pictureID = pictureToShow[index].PictureID;

            using (var context = new Context())
            {
                var querryForCategory = context.Pictures
                    .Where(x => x.PictureID == pictureID)
                    .Select(x => x.Category.Name).ToList();

                TbCategory.Text = querryForCategory.FirstOrDefault();

                var querryForTags = context.PictureTags
                    .Where(x => x.PictureID == pictureID)
                    .Select(x => x.Tag).ToList();

                foreach (var item in querryForTags)
                {
                    TbTags.Text = TbTags.Text + " " + item.Name; 
                }

                var querryForStar = context.Pictures
                    .Where(x => x.PictureID == pictureID)
                    .Select(x => x.Stars).ToList();

                switch (querryForStar.FirstOrDefault())
                {
                    case 1: TbStar.Text = "*"; break;
                    case 2: TbStar.Text = "* *"; break;
                    case 3: TbStar.Text = "* * *"; break;
                    case 4: TbStar.Text = "* * * *"; break;
                    case 5: TbStar.Text = "* * * * *"; break;
                }
            }
        }

        private void BtnEditCategoryOrTag_Click(object sender, RoutedEventArgs e)
        {
            EditCategoryOrTag editWindow = new EditCategoryOrTag();
            editWindow.ShowDialog();
            reloadPage();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IList list = tagsListOfCheckBox.SelectedItems;
        }

        void reloadPage()
        {
            using (var context = new Context())
            {
                List<Tag> listOfTags = context.Tags.Select(x => x).ToList();
                tagsListOfCheckBox.DataContext = listOfTags;

                List<Category> listOfCategorys = context.Categorys.Select(x => x).ToList();
                categoryListOfCheckBox.DataContext = listOfCategorys;
            }
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            index--;
            showPicture();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            index++;
            showPicture();
        }

        private void BtnChoosePath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog(); 
            dlg.DefaultExt = ".png";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                TbLocation.Text = filename;
            }
            ImgPhoto.Source = new BitmapImage(new Uri(TbLocation.Text));
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            hideElement();
            enableClick();
            pictureToShow.Clear();
            index = 0;
            starsListOfCheckBox.SelectionMode = SelectionMode.Multiple;
            categoryListOfCheckBox.SelectionMode = SelectionMode.Multiple;
        }

        private void BtnEditEnable_Click(object sender, RoutedEventArgs e)
        {
            TbCategory.Visibility = Visibility.Hidden;
            TbStar.Visibility = Visibility.Hidden;
            TbTags.Visibility = Visibility.Hidden;
            BtnPrevious.Visibility = Visibility.Hidden;
            BtnNext.Visibility = Visibility.Hidden;
            BtnEditEnable.Visibility = Visibility.Hidden;
            BtnEditPhoto.Visibility = Visibility.Visible;
            BtnDeletePhoto.Visibility = Visibility.Hidden;
            LbCategory.Visibility = Visibility.Hidden;
            categoryListOfCheckBox.IsEnabled = true;
            tagsListOfCheckBox.IsEnabled = true;
            starsListOfCheckBox.IsEnabled = true;
        }

        void hideElement()
        {   
            BtnDeletePhoto.Visibility = Visibility.Hidden;
            BtnChoosePath.Visibility = Visibility.Hidden;
            BtnEditPhoto.Visibility = Visibility.Hidden;
            BtnPrevious.Visibility = Visibility.Hidden;
            BtnNext.Visibility = Visibility.Hidden;
            BtnSavePhoto.Visibility = Visibility.Hidden;
            BtnCancel.Visibility = Visibility.Hidden;
            BtnEditEnable.Visibility = Visibility.Hidden;
            BtnCopyToFolder.Visibility = Visibility.Hidden;

            LbCategory.Visibility = Visibility.Hidden;
            LbPatch.Visibility = Visibility.Hidden;

            ImgPhoto.Visibility = Visibility.Hidden;

            TbDescription.Visibility = Visibility.Hidden;
            TbLocation.Visibility = Visibility.Hidden;
            TbCategory.Visibility = Visibility.Hidden;
            TbStar.Visibility = Visibility.Hidden;
            TbTags.Visibility = Visibility.Hidden;
        }

        void showElement()
        {
            //BtnDeletePhoto.Visibility = Visibility.Visible;
            BtnChoosePath.Visibility = Visibility.Visible;
            //BtnEditPhoto.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Visible;

            LbCategory.Visibility = Visibility.Visible;
            LbPatch.Visibility = Visibility.Visible;

            ImgPhoto.Visibility = Visibility.Visible;

            TbCategory.Visibility = Visibility.Visible;
            TbDescription.Visibility = Visibility.Visible;
            TbLocation.Visibility = Visibility.Visible;
            TbStar.Visibility = Visibility.Visible;
            TbTags.Visibility = Visibility.Visible;
        }

        void disableClick()
        {
            BtnSearchPhoto.IsEnabled = false;
            BtnAddPhoto.IsEnabled = false;
            BtnEditCategoryOrTag.IsEnabled = false;
        }

        void enableClick()
        {
            categoryListOfCheckBox.IsEnabled = true;
            tagsListOfCheckBox.IsEnabled = true;
            starsListOfCheckBox.IsEnabled = true;
            BtnSearchPhoto.IsEnabled = true;
            BtnAddPhoto.IsEnabled = true;
            BtnEditCategoryOrTag.IsEnabled = true;
        }

        void resetValue()
        {
            TbCategory.Text = "";
            TbDescription.Text = "";
            TbLocation.Text = "";
            TbTags.Text = "";
            TbStar.Text = "";
            ImgPhoto.Source = null;
            categoryListOfCheckBox.SelectedItems.Clear();
            tagsListOfCheckBox.SelectedItems.Clear();
            starsListOfCheckBox.SelectedItems.Clear();

        }

        private void BtnCopyToFolder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(TbLocation.Text);
           // string from = "C:\Users\Mateusz\Desktop\cos.bmp";
            string from = "C:\\Users\\Mateusz\\Desktop\\cos.bmp"; //to działa 
            var test = from.Replace(@"\", @"\\").ToString();
            MessageBox.Show(from);

            var to = "C:\\Biblioteka Multimedialna\\cos.bmp";// + Path.GetFileName(TbLocation.Text);
            File.Move(from, to); // Try to move
            MessageBox.Show("Moved 3"); // Success
        }

    }
}