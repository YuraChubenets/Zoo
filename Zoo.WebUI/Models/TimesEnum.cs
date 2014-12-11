using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.WebUI.Models
{
    public static class TimesDict
    {
      public static Dictionary<int, string> dictionaryTime = new Dictionary<int, string>(){  
            {1,"09-00"},
            {2,"08-00___15-00"},
            {3,"08-00___15-00___18-00"},
            {4,"08-00__12-00__16-00__20-00"},
            {5,"08-00__13-00__17-00__21-00__24-00"},
            {6,"08-00__11-00__14-00__17-00__20-00__23-00"}
        };
      
    }
}