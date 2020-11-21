using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class DataObject : ScriptableObject
{
    public float micVolumeNormalized = 0;
    public bool isGameStarted = false;

    public void SetVolume(float newVolume)
    {
        micVolumeNormalized = newVolume;
    }

    public void SetGameStarted(bool toggle)
    {
        isGameStarted = toggle;
    }
}