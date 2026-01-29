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

    private Column _preDragColumn;
    private CardSlot _preDragSlot;

    private Column _dragColumn;

    public CardDatas Datas => _datas;
    public bool IsVisible => _isVisible;

    public Column PreDragColumn{ get => _preDragColumn;  set => _preDragColumn = value; }
    public CardSlot PreDragSlot { set => _preDragSlot = value; }

    public Action<Card> OnRemoveCardEvent;
    public Action OnBeginDragCardEvent;
    public Action<PointerEventData> OnEndDragCardEvent;

    public static Action OnBeginDragEvent;
    public static Action OnEndDragEvent;

    public void Init(CardDatas m_datas, Column dragColumn)
    {
        _datas = m_datas;
        name = m_datas.name;
        _dragColumn = dragColumn;
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
        OnBeginDragEvent?.Invoke();
        OnBeginDragCardEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isVisible)
        {
            return;
        }

        _dragColumn.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isVisible)
        {
            return;
        }
        OnEndDragCardEvent?.Invoke(eventData);
    }

    public void StartDrag()
    {
        OnRemoveCardEvent?.Invoke(this);
        _dragColumn.TryAddCard(this);
    }

    public void EndDrag(PointerEventData eventData)
    {
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
                if (column != null && column.IsBoardColumn)
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

        if (firstColumn != null)
        {
            CardSlot nextFreeSlot = firstColumn.GetNextFreeSlot();
            if (hitSlots.Contains(nextFreeSlot))
            {
                if (firstColumn.TryAddCard(this, false))
                {
                    OnRemoveCardEvent?.Invoke(this);
                    //_preDragColumn?.TryFlipLast();
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
                //_preDragColumn?.TryFlipLast();
                firstSlot.TryAddCard(this);
                return;
            }
        }

        if (_preDragColumn != null)
        {
            if(_preDragColumn.TryAddCard(this, false))
            {
                OnRemoveCardEvent?.Invoke(this);
                _preDragColumn.TryAddCard(this);
                return;
            }
        }
        else
        {
            if(_preDragSlot.TryAddCard(this, false))
            {
                OnRemoveCardEvent?.Invoke(this);
                _preDragSlot.TryAddCard(this);
                return;
            }
        }
    }
}
