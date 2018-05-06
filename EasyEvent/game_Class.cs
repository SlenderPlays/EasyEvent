using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEvent
{
    public class game_Class
    {
        public int value { get; set; }
        public int precentage { get; set; }

        public game_Class(int value, int precentage)
        {
            this.value = value;
            this.precentage = precentage;
        }
    }
}