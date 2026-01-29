using System;
using System.Collections.Generic;
using UnityEngine;

public class Stock : CardSlot
{
    private bool _isShuffleOver;

    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        InstantiateCards(cardDatas, cardPrefab, dragColumn);
        Shuffle();
        return _cards;
    }

    private void InstantiateCards(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        foreach (CardDatas cardData in cardDatas)
        {
            Card card = Instantiate(cardPrefab);
            card.Init(cardData, dragColumn);
            if(TryAddCard(card))
            {
                card.Flip(false);
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
        _isShuffleOver = true;
    }

    protected override bool CanAddCard(Card card)
    {
        return (!_isFull && !_isShuffleOver);
    }
}