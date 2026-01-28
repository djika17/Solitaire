using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cards")]
    [SerializeField] private CardsManager _cardsManager;

    private void Start()
    {
        _cardsManager.Init();
    }
}
