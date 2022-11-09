using System;
using Card;

enum PlayerRole : ushort {
    smallBlind = 1,
    bigBlind = 2,
    nothing = 3
}

public class Player
{
    private PlayerRole role;
    private bool isFolded;
    private int tokens;
    private int tokensBet;
    private Card[] hand;
}

