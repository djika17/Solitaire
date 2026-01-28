using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Column> _columns = new();

    public event Action<int> OnFinishDealCards;

    public void Init(Stack<Card> cards)
    {
        DealCards(cards);
    }

    private void DealCards(Stack<Card> cards)
    {
        int dealedCardsCount = 0;
        foreach (Column col in _columns)
        {
            int cardCountToRemove = col.FillColumn(cards);
            dealedCardsCount += cardCountToRemove;
        }
        OnFinishDealCards?.Invoke(dealedCardsCount);
    }
}
