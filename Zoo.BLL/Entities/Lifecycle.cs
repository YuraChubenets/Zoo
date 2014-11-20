using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Zoo.BLL.Entities
{
   public class Lifecycle
   {
       // ID 
       public int Id { get; set; }

       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
       [DataType(DataType.Date)]
       public DateTime EnteredOrBorn { get; set; }

       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
       [DataType(DataType.Date)]
       public DateTime? TransferredOrDied { get; set; }

   }
}
