using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;

    private CardDatas _datas;
    private bool _isVisible;

    private Transform _currentParent;
    private Transform _dragParent;

    public CardDatas Datas => _datas;
    public bool IsVisible => _isVisible;

    public Action<Card> OnRemoveCardEvent;

    public static Action OnBeginDragEvent;
    public static Action OnEndDragEvent;

    public void Init(CardDatas m_datas, Transform dragParent)
    {
        _datas = m_datas;
        name = m_datas.name;
        _dragParent = dragParent;
        UpdateSprite();
    }

    public void Flip()
    {
        _isVisible = !_isVisible;
        UpdateSprite();
    }

    public void Flip(bool target)
    {
        _isVisible = target;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        _image.sprite = _isVisible ? _datas.Sprite : _datas.BackSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isVisible)
        {
            return;
        }
        _currentParent = transform.parent;
        transform.SetParent(_dragParent, false);
        OnBeginDragEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isVisible)
        {
            return;
        }

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isVisible)
        {
            return;
        }

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        OnEndDragEvent?.Invoke();

        Column firstColumn = null;
        CardSlot firstSlot = null;

        List<CardSlot> hitSlots = new List<CardSlot>();

        foreach (RaycastResult r in results)
        {
            if (firstColumn == null)
            {
                Column column = r.gameObject.GetComponent<Column>();
                if (column != null)
                {
                    firstColumn = column;
                }
            }

            CardSlot slot = r.gameObject.GetComponent<CardSlot>();
            if (slot != null)
            {
                hitSlots.Add(slot);
            }
        }

        if (hitSlots.Count != 0)
        {
            firstSlot = hitSlots[0];
        }

        CardSlot nextFreeSlot = null;
        if (firstColumn != null)
        {
            nextFreeSlot = firstColumn.GetNextFreeSlot();
            if (hitSlots.Contains(firstColumn?.GetNextFreeSlot()))
            {
                if (firstColumn.TryAddCard(this, false))
                {
                    OnRemoveCardEvent?.Invoke(this);
                    firstColumn.TryAddCard(this);
                    return;
                }
            }
        }
        else if (firstSlot != null)
        {
            if (firstSlot.TryAddCard(this, false))
            {
                OnRemoveCardEvent?.Invoke(this);
                firstSlot.TryAddCard(this);
                return;
            }
        }

        transform.SetParent(_currentParent, false);
        transform.localPosition = Vector3.zero;
    }
}
