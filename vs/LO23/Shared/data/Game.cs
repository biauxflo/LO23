using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.data
{
    public class Game
    {
        Round[] rounds { get; set; }
        int turn { set; get; }
        int smallBlind { set; get; }
        int bingBLind { set; get; }
        int currentPLayerIndex { set; get; }
        Phase currentPhase { set; get; }
        int pot { set; get; }
        int highestBet { set; get; }
        int nbNoRise { set; get; }
        ChatMessage[] chat { set; get; }



    }
}
