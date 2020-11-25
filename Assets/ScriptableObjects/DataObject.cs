using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class DataObject : ScriptableObject
{
    public float micVolumeNormalized = 0;
    public bool isGameStarted = false;
    public EasingFunction.Ease colorsEase = EasingFunction.Ease.EaseInOutQuart;

    public void SetVolume(float newVolume)
    {
        micVolumeNormalized = Mathf.Clamp(newVolume, 0, 1);
    }

    public void SetGameStarted(bool toggle)
    {
        isGameStarted = toggle;
    }

    public void SetEase(EasingFunction.Ease ease)
    {
        colorsEase = ease;
    }
}