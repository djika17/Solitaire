using System;
using System.Collections.Generic;
using UnityEngine;

public class Stock : CardSlot
{
    [SerializeField] private Card _cardPrefab;

    public event Action<Stack<Card>> OnStockShuffleEndEvent;

    public void Init(List<CardDatas> cardDatas)
    {
        InstantiateCards(cardDatas);
        Shuffle();
    }

    private void InstantiateCards(List<CardDatas> cardDatas)
    {
        foreach (CardDatas cardData in cardDatas)
        {
            Card card = Instantiate(_cardPrefab);
            card.Init(cardData);
            if (!_isFull)
            {
                AddCard(card);
            }
        }
    }

    private void Shuffle()
    {
        List<Card> list = new List<Card>(_cards);

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        _cards.Clear();

        foreach (Card card in list)
        {
            _cards.Push(card);
        }

        OnStockShuffleEndEvent?.Invoke(_cards);
    }
}