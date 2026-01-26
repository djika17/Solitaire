using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _image;

    private CardDatas _datas;
    private bool _isVisible;

    public void Init(CardDatas m_datas)
    {
        _datas = m_datas;
        name = m_datas.name;
        UpdateSprite();
    }

    public void Flip()
    {
        _isVisible = !_isVisible;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        _image.sprite = _isVisible ? _datas.Sprite : _datas.BackSprite;
    }
}
