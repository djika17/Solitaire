using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [Header("Datas")]
    [SerializeField] private List<CardDatas> _cardDatas = new();

    [Header("Holders")]
    [SerializeField] private Foundations _foundations;
    [SerializeField] private Deck _deck;
    [SerializeField] private Board _board;

    public void Init()
    {
        Stack<Card> shuffleCards = _deck.Init(_cardDatas);
        _board.Init(shuffleCards);
    }
}
