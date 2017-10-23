using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka_Multimedialna.model
{
    public class PictureTag
    {
        [Key, Column(Order = 0)]
        public int PictureID { get; set; }
        [Key, Column(Order = 1)]
        public int TagID { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
