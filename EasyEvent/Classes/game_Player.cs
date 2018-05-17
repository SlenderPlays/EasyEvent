using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEvent
{
    public class game_Player
    {
        public Smod2.API.Player Player { get; set; }
        public bool assigned_to { get; set; }

        public game_Player(Smod2.API.Player Player, bool assigned_to = false)
        {
            this.Player = Player;
            this.assigned_to = assigned_to;
        }
    }
}
