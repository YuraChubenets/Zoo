using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Text;

namespace Zoo.BLL.Entities
{
    public class Feeding
    {

        public int Id { get; set; }
        public int Count { get; set; }

        [Required]
        [Display(Name = "Количество кормлений")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string NameFeeding { get; set; }



    }
}
