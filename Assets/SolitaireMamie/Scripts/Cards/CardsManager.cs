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
        LinkEvents();
        _deck.Init(_cardDatas);
    }

    private void LinkEvents()
    {
        _deck.OnFinishDeckInitEndEvent += InitBoard;
        _board.OnFinishDealCards += FreeStock;
    }

    private void InitBoard(Stack<Card> cards)
    {
        _board.Init(cards);
    }

    private void FreeStock(int cardCount)
    {
        _deck.FreeStock(cardCount);
    }

    private void OnDisable()
    {
        UnlinkEvents();
    }

    private void UnlinkEvents()
    {
        _deck.OnFinishDeckInitEndEvent -= InitBoard;
        _board.OnFinishDealCards += FreeStock;
    }
}
