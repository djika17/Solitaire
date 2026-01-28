using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Column> _columns = new();

    public void Init(Stack<Card> cards)
    {
        DealCards(cards);
    }

    private void DealCards(Stack<Card> cards)
    {
        foreach (Column col in _columns)
        {
            col.FillColumn(cards);
        }
    }
}
