using System;

namespace Shared.data
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
        ) {
            this.index = index;
            this.color = color;
            this.value = value;
            this.isInHand = isInHand;
            this.isHidden = isHidden;
        }
    }
}
