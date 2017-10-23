using Biblioteka_Multimedialna.model;
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

namespace Biblioteka_Multimedialna.View
{
    /// <summary>
    /// Interaction logic for EditCategoryOrTag.xaml
    /// </summary>
    public partial class EditCategoryOrTag : Window
    {
        public EditCategoryOrTag()
        {
            InitializeComponent();

            reloadPage();
        }

        private void BtnTagRename_Click(object sender, RoutedEventArgs e)
        {
            if (tagsListOfCheckBox.SelectedItem == null || TbTagRename.Text == "")
            {
                MessageBox.Show("Nie wybrano żadnego Tagu lub pole zmień nazwe jest puste!");
            }

            else
            {

                 using (var context = new Context())
                 {
                    var selectTag = (Tag)tagsListOfCheckBox.SelectedItem;
                    string tagToRename = selectTag.Name;
                    var querry = context.Tags.Single(x => x.Name == tagToRename);
                    querry.Name = TbTagRename.Text;
                    context.SaveChanges();
                 }
                 reloadPage();
            }
        }

        private void BtnTagDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                if (tagsListOfCheckBox.SelectedItem == null)
                {
                    MessageBox.Show("Nie wybrano żadnego Tagu!");
                }
                else
                {
                var selectTag = (Tag)tagsListOfCheckBox.SelectedItem;
                string tagToRename = selectTag.Name;
                var querry = context.Tags.Single(x => x.Name == tagToRename);
                context.Tags.Remove(querry);
                context.SaveChanges();
                }
            }
            reloadPage();
        }

        private void BtnTagAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbTagAdd.Text == "")
            {
                MessageBox.Show("Wpisz nazwę nowego Tagu!");
            }

            else
            {

            var tagToAdd = new Tag { Name = TbTagAdd.Text };

            using (var context = new Context())
            {
                context.Tags.Add(tagToAdd);
                context.SaveChanges();
            }
            TbTagAdd.Text = "";
            reloadPage();
            }
        }

        private void BtnCategoryRename_Click(object sender, RoutedEventArgs e)
        {
            if (categoryListOfCheckBox.SelectedItem == null || TbCategoryRename.Text == "")
            {
                MessageBox.Show("Nie wybrano żadnej kategorii lub pole zmień nazwe jest puste!");
            }

            else
            {
                using (var context = new Context())
                {
                    var selectCategory = (Category)categoryListOfCheckBox.SelectedItem;
                    string categoryToRename = selectCategory.Name;
                    var querry = context.Categorys.Single(x => x.Name == categoryToRename);
                    querry.Name = TbCategoryRename.Text;
                    context.SaveChanges();
                }
                reloadPage();
            }
        }

        private void BtnCategoryAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbCategoryAdd.Text == "")
            {
                MessageBox.Show("Wpisz nazwę nowej kategorii!");
            }

            else
            {
                var categoryToAdd = new Category { Name = TbCategoryAdd.Text };

                using (var context = new Context())
                {
                    context.Categorys.Add(categoryToAdd);
                    context.SaveChanges();
                }
                TbCategoryAdd.Text = "";
                reloadPage();
            }
        }

        private void BtnCategoryDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                if (categoryListOfCheckBox.SelectedItem == null)
                {
                    MessageBox.Show("Nie wybrano żadnej kategorii!");
                }
                else
                {
                    var selectCategory = (Category)categoryListOfCheckBox.SelectedItem;
                    string categoryToRename = selectCategory.Name;
                    var querry = context.Categorys.Single(x => x.Name == categoryToRename);
                    context.Categorys.Remove(querry);
                    context.SaveChanges();
                }
            }
            reloadPage();
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

        private void BtnBackToAlbum_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
