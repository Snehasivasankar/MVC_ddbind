using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_ddbind.Models
{
    public class stclass
    {
        public int sId { get; set; }
        public string sName { get; set; }
    }
    public class Dclass
    {
        public int dId { get; set; }
        public string dName { get; set; }
    }
    public class UserInsert
    {
        public int sId { get; set;}
        public string sName { get; set; }
        public int dId { get; set; }
        public string dName { get; set; }
        [Required(ErrorMessage ="Enter the Name")]
        public string Name { get; set; }
        [Range(18,50,ErrorMessage ="Enter the age")]
        public int Age { get; set; }
        public string Photo { get; set; }
        public string msg { get; set; }
    }
}