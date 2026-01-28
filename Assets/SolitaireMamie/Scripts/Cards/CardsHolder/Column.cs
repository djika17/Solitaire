using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField, Range(1, 7)] private int _startCardCount;
    [SerializeField] private List<CardSlot> _boardSlots = new();

    private int _cardsCount;

    public void FillColumn(Stack<Card> cards)
    {
        Card cardToAdd = null;
        while (!IsFull() && cards.Count != 0)
        {
            cardToAdd = cards.Pop();
            AddCard(cardToAdd);
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

    private void AddCard(Card card)
    {
        if(_cardsCount <= _boardSlots.Count)
        {
            CardSlot nextSlot = _boardSlots[_cardsCount];
            nextSlot.AddCard(card);
            _cardsCount++;
        }
    }
}
