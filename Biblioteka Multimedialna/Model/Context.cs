using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Biblioteka_Multimedialna.model
{
    public class Context : DbContext
    {
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PictureTag> PictureTags { get; set; }
    }
}
