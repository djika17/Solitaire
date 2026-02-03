using System;
using System.Collections.Generic;
using UnityEngine;

public class Foundations : MonoBehaviour
{
    [SerializeField] private List<FoundationSlot> _foundationSlots = new();

    public Action OnGameEndedEvent;
    public Action OnPlayedCoupEvent;
    
    private void Start()
    {
        foreach (FoundationSlot slot in _foundationSlots) 
        {
            slot.OnFoundationSlotFullEvent += TestGameEnd;
            slot.OnPlayedCoupEvent += OnPlayedCoup;
        }
    }

    private void TestGameEnd()
    {
        foreach (FoundationSlot slot in _foundationSlots)
        {
            if (!slot.IsFull)
            {
                return;
            }
        }
        OnGameEndedEvent?.Invoke();
    }

    private void OnPlayedCoup()
    {
        OnPlayedCoupEvent?.Invoke();
    }

    private void OnDisable()
    {
        foreach (FoundationSlot slot in _foundationSlots)
        {
            slot.OnFoundationSlotFullEvent -= TestGameEnd;
            slot.OnPlayedCoupEvent -= OnPlayedCoup;
        }
    }
}
