using System;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [Header("Datas")]
    [SerializeField] private List<CardDatas> _cardDatas = new();
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private Column _dragColumn;

    [Header("Holders")]
    [SerializeField] private Foundations _foundations;
    [SerializeField] private Deck _deck;
    [SerializeField] private Board _board;

    public Action OnGameEndedEvent;
    public Action OnPlayedCoupEvent;

    public void Init()
    {
        _foundations.OnGameEndedEvent += OnGameEnded;
        _foundations.OnPlayedCoupEvent += OnPlayedCoup;
        _board.OnPlayedCoupEvent += OnPlayedCoup;
        _deck.OnCoupPlayedEvent += OnPlayedCoup;
        Stack<Card> shuffledCards = _deck.Init(_cardDatas, _cardPrefab, _dragColumn);
        _board.Init(shuffledCards);
    }

    private void OnGameEnded()
    {
        OnGameEndedEvent?.Invoke();
    }

    private void OnPlayedCoup()
    {
        OnPlayedCoupEvent?.Invoke();
    }

    private void OnDisable()
    {
        _foundations.OnGameEndedEvent -= OnGameEnded;
        _foundations.OnPlayedCoupEvent -= OnPlayedCoup;
        _board.OnPlayedCoupEvent -= OnPlayedCoup;
        _deck.OnCoupPlayedEvent -= OnPlayedCoup;
    }
}