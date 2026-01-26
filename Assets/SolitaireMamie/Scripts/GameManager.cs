using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private List<CardDatas> _cardDatas = new();
    [SerializeField] private RectTransform _stock;
    [SerializeField] private Card _cardPrefab;

    [Header("Columns")]
    [SerializeField] private List<Column> _columns = new();


    private void Start()
    {
        InstantiateCards();
        DealCards();
    }

    private void InstantiateCards()
    {
        foreach (CardDatas cardDatas in _cardDatas)
        {
            Card card = Instantiate(_cardPrefab, _stock);
            card.Init(cardDatas);
        }
    }

    private void DealCards()
    {
        foreach(Column col in _columns)
        {
            while (!col.IsFull)
            {
                //AddCard
            }
        }
    }

}
