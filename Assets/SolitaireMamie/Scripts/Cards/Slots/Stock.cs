using System;
using System.Collections.Generic;
using UnityEngine;

public class Stock : CardSlot
{
    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Transform dragParent)
    {
        InstantiateCards(cardDatas, cardPrefab, dragParent);
        Shuffle();
        return _cards;
    }

    private void InstantiateCards(List<CardDatas> cardDatas, Card cardPrefab, Transform dragParent)
    {
        foreach (CardDatas cardData in cardDatas)
        {
            Card card = Instantiate(cardPrefab);
            card.Init(cardData, dragParent);
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
    }

    protected override bool CanAddCard(Card card)
    {
        return true;
    }
}