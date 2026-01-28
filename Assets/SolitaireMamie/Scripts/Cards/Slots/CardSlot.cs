using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private int _maxCardsInSlot;

    protected Stack<Card> _cards = new();
    protected bool _isFull = false;

    public bool IsFull => _isFull;

    public void AddCard(Card card)
    {
        _cards.Push(card);
        if (_cards.Count == _maxCardsInSlot)
        {
            _isFull = true;
        }
        card.transform.SetParent(transform, false);
    }

    public void DrawLastCards(int cardCount)
    {
        for (int i = 0; i <= cardCount; i++)
        {
            DrawLastCard();
        }
    }

    public void DrawLastCard()
    {
        if (_cards.Count == 0)
            return;
        _cards.Pop();
    }
}
