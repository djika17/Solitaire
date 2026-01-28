using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class CardSlot : MonoBehaviour
{
    [SerializeField] private int _maxCardsInSlot;

    protected Stack<Card> _cards = new();
    protected bool _isFull = false;

    public bool TryAddCard(Card card)
    {
        if (_isFull || !CanAddCard(card))
        {
            return false;
        }

        _cards.Push(card);

        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;

        card.OnRemoveCardEvent += OnRemoveCard;

        if (_cards.Count == _maxCardsInSlot)
        {
            _isFull = true;
        }

        return true;
    }

    protected abstract bool CanAddCard(Card card);

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

    private void OnRemoveCard(Card cardToRemove)
    {
        _cards.Pop();
        cardToRemove.OnRemoveCardEvent -= OnRemoveCard;
    }

    public Card GetLastCard()
    {
            return _cards.Peek();
    }
}
