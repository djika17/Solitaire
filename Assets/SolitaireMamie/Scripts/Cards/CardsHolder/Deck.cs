using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private CardSlot _waste;

    private void Start()
    {
        _stock.OnSlotPointerClickEvent += OnStockPointerClick;
    }

    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        return _stock.Init(cardDatas, cardPrefab, dragColumn);
    }

    private void OnStockPointerClick()
    {
        Card card = _stock.Cards.Peek();
        card.OnRemoveCardEvent?.Invoke(card);
        _waste.TryAddCard(card);
        card.Flip(true);
    }

    private void OnDisable()
    {
        _stock.OnSlotPointerClickEvent -= OnStockPointerClick;
    }
}
