using UnityEngine;

public class BoardSlot : CardSlot
{
    protected override bool CanAddCard(Card card)
    {
        return true;
    }
}
