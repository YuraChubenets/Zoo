using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zoo.DAL.Entities
{
   public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int IdGender { get; set; }
        public Gender Gender { get; set; }

        public int IdDepartment { get; set; }
        public Department Department { get; set; }

        public int NumderFeeding { get; set; }
        public string DiscriptionFeed { get; set; }

        public int IdUser { get; set; }
        public User USer { get; set; }
   }
}
