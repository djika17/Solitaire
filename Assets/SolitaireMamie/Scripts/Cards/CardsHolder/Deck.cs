using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private CardSlot _waste;

    public Stack<Card> Init(List<CardDatas> cardDatas)
    {
        return _stock.Init(cardDatas);
    }

    public void FreeStock(int cardCount)
    {
        _stock.DrawLastCards(cardCount);
    }
}
