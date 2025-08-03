using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float duration = 300f;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    private bool isRunning = false;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;
            timeRemaining = Mathf.Clamp(timeRemaining, 0f, duration);

            float fill = timeRemaining / duration;
            fillImage.fillAmount = fill;

            UpdateTimerText(timeRemaining);

            if (timeRemaining <= 0f)
            {
                isRunning = false;
                timerText.text = "00:00";
                OnTimerEnd();
            }
        }
    }

    public void StartTimer()
    {
        timeRemaining = duration;
        isRunning = true;
        fillImage.fillAmount = 1f;
        UpdateTimerText(timeRemaining);
    }

    void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        StartTimer();
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timer complete.");
    }
}
