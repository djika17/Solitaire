using System;
using UnityEngine;

public class BoardSlot : CardSlot
{
    public Action OnEmptyBoardSlotEvent;

    protected override bool CanAddCard(Card card)
    {
        return !_isFull;
    }

    protected override void OnRemoveCard(Card cardToRemove)
    {
        base.OnRemoveCard(cardToRemove);
        OnEmptyBoardSlotEvent?.Invoke();
    }
}
