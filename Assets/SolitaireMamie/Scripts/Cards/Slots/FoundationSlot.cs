using UnityEngine;

public class FoundationSlot : CardSlot
{
    [SerializeField] private CardSuit _suit;

    protected override bool CanAddCard(Card card)
    {
        if(_isFull)
        {
            return false;
        }

        CardDatas datas = card.Datas;
        if (_cards.Count == 0) 
        {
            return (datas.Suit == _suit && datas.Value == 1);
        }
        else
        {
            int targetValue = _cards.Peek().Datas.Value + 1;
            return (datas.Suit == _suit && datas.Value == targetValue);
        }
    }
}
