using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cards")]
    [SerializeField] private CardsManager _cardsManager;

    [Header("Texts")]
    [SerializeField] private TextsManager _textsManager;

    private void Start()
    {
        _cardsManager.OnGameEndedEvent += OnGameEnded;
        _cardsManager.OnPlayedCoupEvent += OnPlayedCoup;
        StartCoroutine(InitCoroutine());
    }

    private IEnumerator InitCoroutine()
    {
        yield return null;
        _cardsManager.Init();
        _textsManager.Init();
    }

    private void OnGameEnded()
    {
        Debug.Log("GameEnded");
    }

    private void OnPlayedCoup()
    {
        _textsManager.UpdateCoupVisual();
    }

    private void OnDisable()
    {
        _cardsManager.OnGameEndedEvent -= OnGameEnded;
        _cardsManager.OnPlayedCoupEvent += OnPlayedCoup;
    }
}
