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

    public void Init()
    {
        Stack<Card> shuffledCards = _deck.Init(_cardDatas, _cardPrefab, _dragColumn);
        _board.Init(shuffledCards);
    }
}
