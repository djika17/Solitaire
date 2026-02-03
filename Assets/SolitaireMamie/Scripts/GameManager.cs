using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cards")]
    [SerializeField] private CardsManager _cardsManager;

    private void Start()
    {
        _cardsManager.OnGameEndedEvent += OnGameEnded;
        StartCoroutine(InitCoroutine());
    }

    private IEnumerator InitCoroutine()
    {
        yield return null;
        _cardsManager.Init();
    }

    private void OnGameEnded()
    {
        Debug.Log("GameEnded");
    }

    private void OnDisable()
    {
        _cardsManager.OnGameEndedEvent -= OnGameEnded;
    }
}
