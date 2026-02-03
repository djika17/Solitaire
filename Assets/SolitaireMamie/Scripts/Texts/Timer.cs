using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    private float _timer;

    private bool _isTimerRunning = false;

    private void Update()
    {
        if (!_isTimerRunning)
        {
            return;
        }
        _timer += Time.deltaTime;
        UpdateTimerText();
    }

    public void Init()
    {
        _isTimerRunning = true;
    }

    private void UpdateTimerText()
    {
        int secondsCount = Mathf.RoundToInt(_timer - 0.5f);
        int minuteCount = secondsCount / 60;
        secondsCount = secondsCount % 60;
        string minuteText = GetTwoDigitText(minuteCount);
        string secondText = GetTwoDigitText(secondsCount);
        _text.text = minuteText + ":" + secondText;
    }

    private string GetTwoDigitText(int count)
    {
        count = Mathf.Clamp(count, 0, 99);
        if(count < 10)
        {
            return "0"+count.ToString();
        }
        else
        {
            return count.ToString();
        }
    }
}
