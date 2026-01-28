using UnityEngine;

public enum CardColor
{
    Black,
    Red
}

public enum CardSuit
{
    Club,
    Spade,
    Diamond,
    Heart
}

[CreateAssetMenu()]
public class CardDatas : ScriptableObject
{
    [SerializeField] private CardSuit _suit;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _backSprite;
    [SerializeField, Range(1, 13)] private int _value;

    private CardColor _color;

    public CardColor Color => GetColor();
    public CardSuit Suit => _suit;
    public Sprite Sprite => _sprite;
    public Sprite BackSprite => _backSprite;
    public int Value => _value;

    private CardColor GetColor()
    {
        return (_suit == CardSuit.Club || _suit == CardSuit.Spade) ? CardColor.Black : CardColor.Red;
    }
}
