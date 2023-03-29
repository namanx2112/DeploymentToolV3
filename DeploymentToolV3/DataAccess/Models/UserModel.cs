using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserModel
    {
        
            [Key]
            public int? aUserID { get; set; }

            [Required]
            [StringLength(255)]
            public string tName { get; set; }

            [Required]
            [StringLength(255)]
            public string tUserName { get; set; }            

            [StringLength(255)]
            [EmailAddress]
            public string tEmail { get; set; }

            public int? nDepartment { get; set; }

            public int? nRole { get; set; }

            [StringLength(255)]
            public string tEmpID { get; set; }

            [StringLength(255)]
            [Phone]
            public string tMobile { get; set; }

            public DateTime? dLastLoggedInTime { get; set; }

            public int? nCreatedBy { get; set; }

            public int? nUpdateBy { get; set; }

            public DateTime? dtCreatedOn { get; set; }

            public DateTime? dtUpdatedOn { get; set; }

            public bool? bDeleted { get; set; }

            public List<int>? nBrandID { get; set; }

            public List<int>? nFunctionID { get; set; }


        

    }
}
