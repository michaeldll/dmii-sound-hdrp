using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[System.Serializable]
public class FogChangeEvent : UnityEvent<int, Color, float>
{
}

public class FogCheckpoint : MonoBehaviour
{
    [SerializeField]
    Color fogColor = Color.black;

    [SerializeField]
    float fogDensity = 0.02f;

    [SerializeField]
    float duration = 1f;

    [SerializeField]
    Navigation nav;

    private FogChangeEvent _e;

    void TweenFog(int id, Color color, float density)
    {
        if (!RenderSettings.fog) RenderSettings.fog = true;
        string destinationWorldName = "World_" + id.ToString();
        GameObject _destinationWorldGameObject = GameObject.Find(destinationWorldName);
        World _destinationWorld = _destinationWorldGameObject.GetComponent<World>();
        DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, _destinationWorld.fogDensity, duration);
        DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, _destinationWorld.fogColor, duration);
    }

    private void Start()
    {
        if (_e == null)
            _e = new FogChangeEvent();

        _e.AddListener(TweenFog);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") _e.Invoke(nav.next, fogColor, fogDensity);
    }
}
