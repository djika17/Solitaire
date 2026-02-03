using TMPro;
using UnityEngine;

public class TryCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _count;

    public void UpdateCoup()
    {
        _count++;
        _text.text = _count.ToString();
    }
}
