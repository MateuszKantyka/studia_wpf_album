using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka_Multimedialna.model
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public string Image { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<PictureTag> PictureTags { get; set; }
    }
}
