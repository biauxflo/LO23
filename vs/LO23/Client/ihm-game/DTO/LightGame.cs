using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Classes temporaires à remplacer par les classes de DATA
namespace Client.ihm_game.DTO
{
    public class Card
    {
        public int index { get; set; }
        public char color { get; set; }
        public int value { get; set; }
        public bool isInHand { get; set; }
        public bool isHidden { get; set; }

        public Card(
            int index,
            char color,
            int value,
            bool isInHand,
            bool isHidden
        )
        {
            this.index = index;
            this.color = color;
            this.value = value;
            this.isInHand = isInHand;
            this.isHidden = isHidden;
        }
    }

    enum PlayerRole
    {
        smallBlind,
        bigBlind,
        nothing,
    }



    class Player
    {
        public string role { get; set; }

        public bool isFolded { get; set; }
        public int tokens { get; set; }
        public int tokensBet { get; set; }
        public List<Card> hand { get; set; }
        public string username { get; set; }
        public string image { get; set; }
        public Player(int tokens,string username, string image)
        {
            this.role = PlayerRole.nothing.ToString();
            this.isFolded = false;
            this.tokens = tokens;
            this.tokensBet = 0;
            this.hand = new List<Card>();
            this.username = username;
            this.image = image;
        }


    }
    internal class LightGame
    {
        public int id { get; set; }
        public int indexRound { get; set; }
        public List<Player> players { get; set; }
        public int pot { set; get; }

        public LightGame(int id, List<Player> players, int pot)
        {
            this.id = id;
            this.indexRound = 0;
            this.players = players;
            this.pot = pot;
        }


    }
}
