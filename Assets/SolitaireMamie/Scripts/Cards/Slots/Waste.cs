using UnityEngine;

public class Waste : CardSlot
{
    protected override bool CanAddCard(Card card)
    {
        return false;
    }
}
