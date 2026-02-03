using System;
using System.Collections.Generic;
using UnityEngine;

public class Foundations : MonoBehaviour
{
    [SerializeField] private List<FoundationSlot> _foundationSlots = new();

    public Action OnGameEndedEvent;
    
    private void Start()
    {
        foreach (FoundationSlot slot in _foundationSlots) 
        {
            slot.OnFoundationSlotFullEvent += TestGameEnd;
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

    private void OnDisable()
    {
        foreach (FoundationSlot slot in _foundationSlots)
        {
            slot.OnFoundationSlotFullEvent -= TestGameEnd;
        }
    }
}
