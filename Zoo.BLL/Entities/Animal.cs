using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Zoo.BLL.Entities
{
   public class Animal
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Вид животного")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string KindOfAnimal { get; set; }

        [Required]
        [Display(Name = "Пол животного")] 
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

       
        [Required]
        [Display(Name = "Отдел зоопарка")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        [Display(Name = "Количество приемов пищи в сутки")]
        [Range(1,24, ErrorMessage = "Pades very big or negative ")]
        [Required(ErrorMessage = "Обязотельное поле.")]
        public int NumberFeeding { get; set; }
        
       
        [Required]
        [Display(Name = "Описание рациона")]
        public string DiscriptionFeed { get; set; }

        [Required]
        [Display(Name = "Пользователь введший животного")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "График кормления")]
        public int FeedingId { get; set; }
        public Feeding Feeding { get; set; }

        [Required]
        [Display(Name = "Дата поступления")]
        public int LifecycleId { get; set; }
        public Lifecycle Lifecycles { get; set; }

   }
}
