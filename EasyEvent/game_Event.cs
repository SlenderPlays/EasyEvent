using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEvent
{
    public class game_Event
    {
        public string name { get; set; }
        public List<game_Class> classes { get; set; }
        public game_Event(string name , List<game_Class> classes)
        {
            this.name = name;
            this.classes = classes;
        }
    }
}
