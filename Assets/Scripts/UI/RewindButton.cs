using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RewindButton : MonoBehaviour
{
    [SerializeField] Image foreground;
    [SerializeField] Image background;
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float scaleFactor = 0.8f;
    [SerializeField] private float scaleDuration = 0.8f;

    private RectTransform fgRect;
    private RectTransform bgRect;

    void Start()
    {
        fgRect = foreground.GetComponent<RectTransform>();
        bgRect = background.GetComponent<RectTransform>();

        StartClock();
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RewindClock();
        }
        else
        {
            StartClock();
        }
    }

    void RotateHands(bool rotateClockwise)
    {
        float rotationAngle = rotateClockwise ? -360f : 360f;

        LeanTween.rotateAroundLocal(fgRect.gameObject, Vector3.forward, rotationAngle, rotationDuration)
                 .setRepeat(-1) // Loop forever
                 .setEase(LeanTweenType.linear);
    }

    void StartClock()
    {
        RotateHands(true);

        Vector3 originalScale = bgRect.localScale;
        Vector3 newScale = originalScale * scaleFactor;

        LeanTween.scale(bgRect, newScale, scaleDuration)
             .setEaseInOutSine()
             .setLoopPingPong();
    }

    void RewindClock()
    {
        RotateHands(false);
        LeanTween.cancel(bgRect);
        LeanTween.scale(bgRect, Vector3.one, 0.2f); 
    }
}
