using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stock : CardSlot, IPointerClickHandler
{
    private bool _canAddCards;

    public Action OnClickOnStockEvent;

    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        InstantiateCards(cardDatas, cardPrefab, dragColumn);
        Shuffle();
        return _cards;
    }

    public void OnEndWasteAdd()
    {
        _canAddCards = false;
        //Shuffle();
        OnCardPointerClickEvent?.Invoke();
    }

    private void InstantiateCards(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        _canAddCards = true;
        foreach (CardDatas cardData in cardDatas)
        {
            Card card = Instantiate(cardPrefab);
            card.Init(cardData, dragColumn);
            if(TryAddCard(card))
            {
                card.Flip(false);
            }
        }
        _canAddCards = false;
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
        return (_canAddCards && !_isFull);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _canAddCards = true;
        OnClickOnStockEvent?.Invoke();
    }
}