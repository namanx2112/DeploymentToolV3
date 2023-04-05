using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BrandModel
    {
        [Key]
        public int tBrandId { get; set; }

        [StringLength(255)]
        public string tBrandName { get; set; }

        public string tBrandDescription { get; set; }

        [StringLength(255)]
        public string tBrandWebsite { get; set; }

        [StringLength(255)]
        public string tBrandCountry { get; set; }

        public DateTime? tBrandEstablished { get; set; }

        [StringLength(255)]
        public string tBrandCategory { get; set; }

        [StringLength(255)]
        public string tBrandContact { get; set; }

        public int? nBrandLogoAttachmentID { get; set; }

        public int? nCreatedBy { get; set; }

        public int? nUpdateBy { get; set; }

        public DateTime? dtCreatedOn { get; set; }

        public DateTime? dtUpdatedOn { get; set; }

        public bool? bDeleted { get; set; }
        public int? nUserID { get; set; }
    }
}
