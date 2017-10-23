using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka_Multimedialna.model
{

    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PictureTag> PictureTags { get; set; }
    }
}
