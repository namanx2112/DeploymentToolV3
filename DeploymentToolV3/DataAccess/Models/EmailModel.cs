﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class EmailModel
    {
        
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}
