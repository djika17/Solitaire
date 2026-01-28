using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private CardSlot _waste;

    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Transform dragParent)
    {
        return _stock.Init(cardDatas, cardPrefab, dragParent);
    }
}
