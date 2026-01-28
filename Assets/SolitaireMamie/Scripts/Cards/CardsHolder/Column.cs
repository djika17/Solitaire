using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField, Range(1, 7)] private int _startCardCount;
    [SerializeField] private List<BoardSlot> _boardSlots = new();

    private int _cardsCount;

    public void FillColumn(Stack<Card> cards)
    {
        Card cardToAdd = null;
        while (!IsFull() && cards.Count != 0)
        {
            cardToAdd = cards.Pop();
            TryAddCard(cardToAdd);
        }

        if (cardToAdd != null)
        {
            cardToAdd.Flip();
        }
    }

    private bool IsFull()
    {
        return _cardsCount >= _startCardCount;
    }

    public bool TryAddCard(Card card)
    {
        if (_cardsCount > _boardSlots.Count)
        {
            return false;
        }

        if (!card.IsVisible)
        {
            return TryAddCardToSlot(card);
        }

        CardDatas cardDatas = card.Datas;

        if (_cardsCount == 0)
        {
            if (cardDatas.Value == 13)
            {
                return TryAddCardToSlot(card);
            }
        }

        if (_cardsCount <= _boardSlots.Count)
        {
            BoardSlot currentSlot = _boardSlots[_cardsCount - 1];
            CardDatas currentCardDatas = currentSlot.GetLastCard().Datas;

            int currentValue = currentCardDatas.Value;
            CardColor currentColor = currentCardDatas.Color;

            int targetValue = currentValue - 1;
            CardColor targetColor = (currentColor == CardColor.Black) ? CardColor.Red : CardColor.Black;

            if (cardDatas.Value == targetValue && cardDatas.Color == targetColor) 
            {
                return TryAddCardToSlot(card);
            }
        }

        return false;
    }

    private bool TryAddCardToSlot(Card card)
    {
        CardSlot nextSlot = _boardSlots[_cardsCount];
        if (nextSlot.TryAddCard(card))
        {
            _cardsCount++;
            return true;
        }
        return false;
    }
}
