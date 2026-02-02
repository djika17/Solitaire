using System;
using UnityEngine;

public class BoardSlot : CardSlot
{
    public Action OnEmptyBoardSlotEvent;

    private void Start()
    {
        Card.OnBeginDragEvent += OnBeginDrag;
        Card.OnEndDragEvent += OnEndDrag;
    }

    protected override bool CanAddCard(Card card)
    {
        return !_isFull;
    }

    protected override void OnRemoveCard(Card cardToRemove)
    {
        base.OnRemoveCard(cardToRemove);
        OnEmptyBoardSlotEvent?.Invoke();
    }

    private void OnBeginDrag()
    {
        if (_image != null)
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

    private void OnDisable()
    {
        Card.OnBeginDragEvent -= OnBeginDrag;
        Card.OnEndDragEvent -= OnEndDrag;
    }
}
