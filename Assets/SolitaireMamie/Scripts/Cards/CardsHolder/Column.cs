using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField, Range(1, 7)] private int _startCardCount;
    [SerializeField] private List<BoardSlot> _boardSlots = new();

    private int _cardsCount;

    private void Start()
    {
        foreach (BoardSlot slot in _boardSlots) 
        {
            slot.OnEmptyBoardSlotEvent += OnEmptyBoardSlot;
        }
    }

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

    public CardSlot GetNextFreeSlot()
    {
        return _boardSlots[_cardsCount];
    }

    private bool IsFull()
    {
        return _cardsCount >= _startCardCount;
    }

    public bool TryAddCard(Card card, bool addCard = true)
    {
        if (_cardsCount > _boardSlots.Count)
        {
            return false;
        }

        if (!card.IsVisible)
        {
            return TryAddCardToSlot(card, addCard);
        }

        CardDatas cardDatas = card.Datas;

        if (_cardsCount == 0)
        {
            if (cardDatas.Value == 13)
            {
                return TryAddCardToSlot(card, addCard);
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
                return TryAddCardToSlot(card, addCard);
            }
        }

        return false;
    }

    private bool TryAddCardToSlot(Card card, bool addCard = true)
    {
        CardSlot nextSlot = GetNextFreeSlot();
        if (nextSlot.TryAddCard(card, addCard))
        {
            if (addCard)
            {
                _cardsCount++;
            }
            return true;
        }
        return false;
    }

    private void OnEmptyBoardSlot()
    {
        _cardsCount--;
    }

    private void OnDisable()
    {
        foreach (BoardSlot slot in _boardSlots) 
        {
            slot.OnEmptyBoardSlotEvent -= OnEmptyBoardSlot;
        }
    }
}
