﻿using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.Models
{
  public partial  class District:Entity
    {
       
        public List<KeyValuePair<int, string>> kvpDistrict { get; set; }
      
       
    }
}