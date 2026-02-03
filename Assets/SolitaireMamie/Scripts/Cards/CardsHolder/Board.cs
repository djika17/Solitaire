using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Column> _columns = new();

    public Action OnPlayedCoupEvent;

    private void Start()
    {
        foreach(Column column in _columns)
        {
            column.OnPlayedCoupEvent += OnPlayedCoup;
        }
    }

    public void Init(Stack<Card> cards)
    {
        DealCards(cards);
    }

    private void DealCards(Stack<Card> cards)
    {
        StartCoroutine(DealCardsCoroutine(cards));
    }

    private IEnumerator DealCardsCoroutine(Stack<Card> cards)
    {
        for (int i = 0; i<_columns.Count; i ++)
        {
            _columns[i].FillColumn(cards);
            yield return new WaitForSeconds(.05f);
        }
    }

    private void OnPlayedCoup()
    {
        OnPlayedCoupEvent?.Invoke();
    }

    private void OnDisable()
    {
        foreach (Column column in _columns)
        {
            column.OnPlayedCoupEvent -= OnPlayedCoup;
        }
    }
}
