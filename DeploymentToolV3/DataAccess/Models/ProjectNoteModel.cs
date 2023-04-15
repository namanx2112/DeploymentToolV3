using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ProjectNoteModel    
    {
        [Key]
        public int aNoteID { get; set; }

        public int? nProjectID { get; set; }

        public int? nStoreID { get; set; }

        public int? nNoteType { get; set; }

        [StringLength(255)]
        public string tSource { get; set; }

        [StringLength(255)]
        public string tNoteDescription { get; set; }

        public int? nCreatedBy { get; set; }

        public int? nUpdatedBy { get; set; }

        public DateTime? dtCreatedOn { get; set; }

        public DateTime? dtUpdatedOn { get; set; }

        public bool? bDeleted { get; set; }
        public int? nUserID { get; set; }
    }
}
