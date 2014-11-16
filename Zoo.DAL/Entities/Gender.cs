using System;
using System.ComponentModel.DataAnnotations;


namespace Zoo.DAL.Entities
{
    public class Gender
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Пол животного")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
    }
}
