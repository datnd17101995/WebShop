using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    [Table("Error")]
    public class Error
    {
        [key]
        public int Id { get;set; }

        public string Messeage { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
