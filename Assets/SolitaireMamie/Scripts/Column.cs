using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField, Range(1, 7)] private int _maxCardCount;

    public int MaxCardCound => _maxCardCount;

    public bool IsFull => transform.childCount >= _maxCardCount;
}
