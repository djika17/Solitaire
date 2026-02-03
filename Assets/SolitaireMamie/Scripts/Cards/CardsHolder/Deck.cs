using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Stock _stock;
    [SerializeField] private Waste _waste;

    private float _stockWasteMoveDuration;

    public Action OnCoupPlayedEvent;

    private void Start()
    {
        _stock.OnCardPointerClickEvent += OnStockCardPointerClick;
        _stock.OnClickOnStockEvent += OnClickOnStock;
    }

    public Stack<Card> Init(List<CardDatas> cardDatas, Card cardPrefab, Column dragColumn)
    {
        float distance = Vector3.Distance(_stock.transform.position, _waste.transform.position);
        _stockWasteMoveDuration = Mathf.Max(distance / Utilitaries.CardMoveSpeed, Utilitaries.FlipCardDuration);
        return _stock.Init(cardDatas, cardPrefab, dragColumn);
    }

    private void OnStockCardPointerClick()
    {
        Card card = _stock.GetLastCard();
        card.OnRemoveCardEvent?.Invoke(card);
        _waste.TryAddCard(card, shouldPlayAnim:true);
        OnCoupPlayedEvent?.Invoke();
        card.Flip(true);
    }

    private void OnClickOnStock()
    {
        StartCoroutine(FillStockCoroutine());
    }

    private IEnumerator FillStockCoroutine()
    {
        while (!_waste.IsEmpty)
        {
            Card card = _waste.GetLastCard();
            card.OnRemoveCardEvent?.Invoke(card);
            if (_stock.TryAddCard(card, shouldPlayAnim: true))
            {
                card.Flip(false);
            }
        }
        yield return new WaitForSeconds(_stockWasteMoveDuration + 0.1f);
        _stock.OnEndWasteAdd();
    }

    private void OnDisable()
    {
        _stock.OnCardPointerClickEvent -= OnStockCardPointerClick;
        _stock.OnClickOnStockEvent -= OnClickOnStock;
    }
}
