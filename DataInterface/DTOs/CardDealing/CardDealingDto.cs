using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DTOs.CardDealing
{
    public class CardDealingDto
    {
        public string? CardSuit { get; set; }
        public string? CardRanking { get; set; }

        public CardDealingDto(string cardSuit, string cardRanking) 
        {
            CardSuit = cardSuit;
            CardRanking = cardRanking;
        }

        public CardDealingDto() 
        {

        }

    }
}
