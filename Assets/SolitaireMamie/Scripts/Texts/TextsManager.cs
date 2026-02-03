using UnityEngine;

public class TextsManager : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private Timer _timer;
    [SerializeField] private TryCount _tryCount;

    public void Init()
    {
        _timer.Init();
    }

    public void UpdateCoupVisual()
    {
        _tryCount.UpdateCoup();
    }
}
