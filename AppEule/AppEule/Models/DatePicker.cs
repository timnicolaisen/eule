using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppEule.Models
{
    public class DatePicker
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        public DateTime? InsDueDate { get; set; }
    }
}