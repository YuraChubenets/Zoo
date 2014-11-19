using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zoo.BLL.Entities
{
   public class Animal
    {
        public int Id { get; set; }
        public string KindOfAnimal { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int NumberFeeding { get; set; }
        public string DiscriptionFeed { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int FeedingId { get; set; }
        public Feeding Feeding { get; set; }



   }
}
