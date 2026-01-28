using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private CardSlot _waste;

    public event Action<Stack<Card>> OnFinishDeckInitEndEvent;

    public void Init(List<CardDatas> cardDatas)
    {
        LinkEvents();
        _stock.Init(cardDatas);
    }

    public void FreeStock(int cardCount)
    {
        _stock.DrawLastCards(cardCount);
    }

    private void LinkEvents()
    {
        _stock.OnStockShuffleEndEvent += OnStockShuffleEnd;
    }

    private void OnStockShuffleEnd(Stack<Card> cards)
    {
        OnFinishDeckInitEndEvent?.Invoke(cards);
    }

    private void OnDisable()
    {
        UnlinkEvents();
    }

    private void UnlinkEvents()
    {
        _stock.OnStockShuffleEndEvent -= OnStockShuffleEnd;
    }

}
