using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Column> _columns = new();

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
}
