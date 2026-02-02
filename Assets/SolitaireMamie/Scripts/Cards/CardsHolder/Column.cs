using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Column : MonoBehaviour
{
    [SerializeField] private bool _isBoardColumn;
    [SerializeField, Range(1, 7)] private int _startCardCount;
    [SerializeField] private List<BoardSlot> _boardSlots = new();

    private int _cardsCount;

    public bool IsBoardColumn => _isBoardColumn;

    private void Start()
    {
        foreach (BoardSlot slot in _boardSlots) 
        {
            slot.OnEmptyBoardSlotEvent += OnEmptyBoardSlot;
            if (_isBoardColumn)
            {
                slot.OnCardInSlotBeginDragEvent += OnBeginDragCardInSlot;
            }
            else
            {
                slot.OnCardInSlotEndDragEvent += OnEndDragCardInSlot;
            }
        }
    }

    public void FillColumn(Stack<Card> cards)
    {
        Card cardToAdd = null;
        while (!IsFull() && cards.Count != 0)
        {
            cardToAdd = cards.Peek();
            if(TryAddCard(cardToAdd, false))
            {
                cardToAdd.OnRemoveCardEvent?.Invoke(cardToAdd);
                TryAddCard(cardToAdd);
            }
        }

        if (cardToAdd != null)
        {
            cardToAdd.Flip(true);
        }
    }

    public CardSlot GetNextFreeSlot()
    {
        return _boardSlots[_cardsCount];
    }

    public void TryFlipLast()
    {
        if (_cardsCount != 0)
        {
            _boardSlots[_cardsCount - 1].TryFlip();
        }
    }
    
    public bool TryAddCard(Card card, bool addCard = true)
    {
        if (_cardsCount > _boardSlots.Count)
        {
            return false;
        }

        if (!card.IsVisible)
        {
            return TryAddCardToSlot(card, addCard);
        }

        CardDatas cardDatas = card.Datas;

        if(card.PreDragColumn == this)
        {
            return TryAddCardToSlot(card, addCard);
        }

        if (_cardsCount == 0)
        {
            if (!_isBoardColumn)
            {
                return TryAddCardToSlot(card, addCard);
            }
            else if (cardDatas.Value == 13)
            {
                return TryAddCardToSlot(card, addCard);
            }

            return false;
        }
        
        if (_cardsCount <= _boardSlots.Count)
        {
            BoardSlot currentSlot = _boardSlots[_cardsCount - 1];
            CardDatas currentCardDatas = currentSlot.GetLastCard().Datas;

            int currentValue = currentCardDatas.Value;
            CardColor currentColor = currentCardDatas.Color;

            int targetValue = currentValue - 1;
            CardColor targetColor = (currentColor == CardColor.Black) ? CardColor.Red : CardColor.Black;

            if (cardDatas.Value == targetValue && cardDatas.Color == targetColor) 
            {
                return TryAddCardToSlot(card, addCard);
            }
        }

        return false;
    }

    private bool TryAddCardToSlot(Card card, bool addCard = true)
    {
        CardSlot nextSlot = GetNextFreeSlot();
        if (nextSlot.TryAddCard(card, addCard, _isBoardColumn))
        {
            if (addCard)
            {
                _cardsCount++;
                if (_isBoardColumn)
                {
                    card.PreDragColumn = this;
                }
            }
            return true;
        }
        return false;
    }

    private bool IsFull()
    {
        return _cardsCount >= _startCardCount;
    }

    private void OnEmptyBoardSlot()
    {
        _cardsCount--;
    }

    private void OnBeginDragCardInSlot(BoardSlot slot)
    {
        int currentIndex = _boardSlots.IndexOf(slot);
        while (currentIndex < _boardSlots.Count && _boardSlots[currentIndex].IsFull)
        {
            _boardSlots[currentIndex].StartDragCard();
            currentIndex++;
        }
    }

    private void OnEndDragCardInSlot(CardSlot slot, PointerEventData eventData)
    {
        Column targetCol = _boardSlots[0].EndDragCard(eventData, _boardSlots[1].IsFull);
        if (targetCol != null) 
        {
            for (int i = 1; i < _boardSlots.Count; i++) 
            {
                if(_boardSlots[i].IsFull)
                {
                    Card card = _boardSlots[i].GetLastCard();
                    card.OnRemoveCardEvent?.Invoke(card);
                    targetCol.TryAddCard(card);
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (BoardSlot slot in _boardSlots) 
        {
            slot.OnEmptyBoardSlotEvent -= OnEmptyBoardSlot;
            if (_isBoardColumn)
            {
                slot.OnCardInSlotBeginDragEvent -= OnBeginDragCardInSlot;
            }
            else
            {
                slot.OnCardInSlotEndDragEvent -= OnEndDragCardInSlot;
            }
        }
    }
}
