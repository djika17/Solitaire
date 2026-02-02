using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private Waste _waste;

    private void Start()
    {
        _stock.OnCardPointerClickEvent += OnStockCardPointerClick;
        _stock.OnClickOnStockEvent += OnClickOnStock;
    }

    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        return _stock.Init(cardDatas, cardPrefab, dragColumn);
    }

    private void OnStockCardPointerClick()
    {
        Card card = _stock.GetLastCard();
        card.OnRemoveCardEvent?.Invoke(card);
        _waste.TryAddCard(card);
        card.Flip(true);
    }

    private void OnClickOnStock()
    {
        while (!_waste.IsEmpty)
        {
            Card card = _waste.GetLastCard();
            card.OnRemoveCardEvent?.Invoke(card);
            if (_stock.TryAddCard(card))
            {
                card.Flip(false);
            }
        }
        _stock.OnEndWasteAdd();
    }

    private void OnDisable()
    {
        _stock.OnCardPointerClickEvent -= OnStockCardPointerClick;
        _stock.OnClickOnStockEvent -= OnClickOnStock;
    }
}
