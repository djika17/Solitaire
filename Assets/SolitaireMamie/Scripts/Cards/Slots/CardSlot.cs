using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CardSlot : MonoBehaviour
{
    [SerializeField] private int _maxCardsInSlot;
    [SerializeField] private Image _image;

    protected Stack<Card> _cards = new();
    protected bool _isFull = false;

    public bool IsFull => _isFull;

    public Action<CardSlot> OnCardInSlotBeginDragEvent;
    public Action<CardSlot, PointerEventData> OnCardInSlotEndDragEvent;

    private void Start()
    {
        Card.OnBeginDragEvent += OnBeginDrag;
        Card.OnEndDragEvent += OnEndDrag;
    }

    protected abstract bool CanAddCard(Card card);

    public bool TryAddCard(Card card, bool addCard = true, bool addPreDragSlot = true)
    {
        if (!CanAddCard(card))
        {
            return false;
        }

        if (addCard)
        {
            AddCard(card, addPreDragSlot);
        }

        return true;
    }

    public void StartDragCard()
    {
        _cards.Peek().StartDrag();
    }

    public void EndDragCard(PointerEventData eventData)
    {
        _cards.Peek().EndDrag(eventData);
    }

    private void AddCard(Card card, bool addPreDragSlot)
    {
        _cards.Push(card);

        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;

        card.OnRemoveCardEvent += OnRemoveCard;
        card.OnBeginDragCardEvent += OnCardBeginDrag;
        card.OnEndDragCardEvent += OnCardEndDrag;

        if (_cards.Count == _maxCardsInSlot)
        {
            _isFull = true;
        }

        if (addPreDragSlot)
        {
            card.PreDragSlot = this;
            if(!(this as BoardSlot))
            {
                card.PreDragColumn = null;
            }
        }
    }

    protected virtual void OnRemoveCard(Card cardToRemove)
    {
        _cards.Pop();
        _isFull = false;
        cardToRemove.OnRemoveCardEvent -= OnRemoveCard;
        cardToRemove.OnBeginDragCardEvent -= OnCardBeginDrag;
        cardToRemove.OnEndDragCardEvent -= OnCardEndDrag;
    }

    public Card GetLastCard()
    {
        return _cards.Peek();
    }

    public void TryFlip()
    {
        if (!_cards.Peek().IsVisible)
        {
            _cards.Peek().Flip(true);
        }
    }

    private void OnBeginDrag()
    {
        if(_image != null)
        {
            _image.raycastTarget = true;
        }
    }

    private void OnEndDrag()
    {
        if (_image != null)
        {
            _image.raycastTarget = false;
        }
    }

    private void OnCardBeginDrag()
    {
        OnCardInSlotBeginDragEvent?.Invoke(this);
    }

    private void OnCardEndDrag(PointerEventData eventData)
    {
        OnCardInSlotEndDragEvent?.Invoke(this, eventData);
    }
}
