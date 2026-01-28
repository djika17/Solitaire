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

        Column firstColumn = null;
        CardSlot firstSlot = null;

        foreach (RaycastResult r in results)
        {
            if (firstColumn == null)
            {
                firstColumn = r.gameObject.GetComponent<Column>();
            }

            if (firstSlot == null)
            {
                firstSlot = r.gameObject.GetComponent<CardSlot>();
            }

            if (firstColumn != null && firstSlot != null)
                break;
        }

        if (firstColumn != null && firstColumn.TryAddCard(this))
        {
            OnRemoveCardEvent?.Invoke(this);
        }
        else if (firstSlot != null && firstSlot.TryAddCard(this))
        {
            OnRemoveCardEvent?.Invoke(this);
        }
        else
        {
            transform.SetParent(_currentParent, false);
            transform.localPosition = Vector3.zero;
        }
    }
}
