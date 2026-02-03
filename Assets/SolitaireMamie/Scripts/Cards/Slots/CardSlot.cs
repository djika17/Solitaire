using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CardSlot : MonoBehaviour
{
    [SerializeField] private int _maxCardsInSlot;
    [SerializeField] protected Image _image;

    protected Stack<Card> _cards = new();
    protected bool _isFull = false;

    public bool IsFull => _isFull;
    public bool IsEmpty => _cards.Count == 0;

    public Action<BoardSlot> OnCardInSlotBeginDragEvent;
    public Action<CardSlot, PointerEventData> OnCardInSlotEndDragEvent;
    public Action OnCardPointerClickEvent;

    public Action OnFoundationSlotFullEvent;

    public Action OnPlayedCoupEvent;

    protected abstract bool CanAddCard(Card card);

    public bool TryAddCard(Card card, bool addCard = true, bool addPreDragSlot = true, bool shouldPlayAnim = false)
    {
        if (!CanAddCard(card))
        {
            return false;
        }

        if (addCard)
        {
            if (shouldPlayAnim)
            {
                float distance = Vector3.Distance(transform.position, card.transform.position);
                float duration = distance / Utilitaries.CardMoveSpeed;
                card.transform.DOMove(transform.position, duration).SetEase(Ease.OutSine).OnComplete(() => { AddCard(card, addPreDragSlot); });
            }
            else
            {
                AddCard(card, addPreDragSlot);
            }
        }

        return true;
    }

    public void StartDragCard()
    {
        _cards.Peek().StartDrag();
    }

    public Column EndDragCard(PointerEventData eventData, bool hasMoreThanOneCard)
    {
        return _cards.Peek().EndDrag(eventData, hasMoreThanOneCard);
    }

    private void AddCard(Card card, bool addPreDragSlot)
    {
        _cards.Push(card);

        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;

        card.OnRemoveCardEvent += OnRemoveCard;
        card.OnBeginDragCardEvent += OnCardBeginDrag;
        card.OnEndDragCardEvent += OnCardEndDrag;
        card.OnPointerClickEvent += OnCardPointerClick;
        card.OnKingAddedOnSlotEvent += OnKingAdded;
        card.OnPlayedCoupEvent += OnPlayedCoup;

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
        cardToRemove.OnPointerClickEvent -= OnCardPointerClick;
        cardToRemove.OnKingAddedOnSlotEvent -= OnKingAdded;
        cardToRemove.OnPlayedCoupEvent -= OnPlayedCoup;
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

    private void OnCardBeginDrag()
    {
        BoardSlot boardSlot = this as BoardSlot;
        if(boardSlot)
        {
            OnCardInSlotBeginDragEvent?.Invoke(boardSlot);
        }
        else
        {
            StartDragCard();
        }
    }

    private void OnCardEndDrag(PointerEventData eventData)
    {
        OnCardInSlotEndDragEvent?.Invoke(this, eventData);
    }

    private void OnPlayedCoup()
    {
        if(this is BoardSlot || this is FoundationSlot)
        {
            OnPlayedCoupEvent?.Invoke();
        }
    }

    private void OnKingAdded()
    {
        FoundationSlot foundationSlot = this as FoundationSlot;
        if (foundationSlot)
        {
            OnFoundationSlotFullEvent?.Invoke();
        }
    }

    private void OnCardPointerClick()
    {
        OnCardPointerClickEvent?.Invoke();
    }

}
