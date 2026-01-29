using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardSlot : MonoBehaviour
{
    [SerializeField] private int _maxCardsInSlot;
    [SerializeField] private Image _image;

    protected Stack<Card> _cards = new();
    protected bool _isFull = false;

    private void Start()
    {
        Card.OnBeginDragEvent += OnBeginDrag;
        Card.OnEndDragEvent += OnEndDrag;
    }

    protected abstract bool CanAddCard(Card card);

    public bool TryAddCard(Card card, bool addCard = true)
    {
        if (!CanAddCard(card))
        {
            return false;
        }

        if (addCard)
        {
            AddCard(card);
        }

        return true;
    }

    private void AddCard(Card card)
    {
        _cards.Push(card);

        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;

        card.OnRemoveCardEvent += OnRemoveCard;

        if (_cards.Count == _maxCardsInSlot)
        {
            _isFull = true;
        }
    }

    protected virtual void OnRemoveCard(Card cardToRemove)
    {
        _cards.Pop();
        cardToRemove.OnRemoveCardEvent -= OnRemoveCard;
    }

    public Card GetLastCard()
    {
        return _cards.Peek();
    }

    private void OnBeginDrag()
    {
        _image.raycastTarget = true;
    }

    private void OnEndDrag()
    {
        _image.raycastTarget = false;
    }
}
