using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    bool getNextWorldFog = true;

    private bool _hasTriggered = false;
    private FogChangeEvent _e;
    private delegate void TimeoutCallback();

    void TweenFog(int id, Color color, float density)
    {
        if (!RenderSettings.fog) RenderSettings.fog = true;
        if (getNextWorldFog)
        {
            string destinationWorldName = "World_" + id.ToString();
            GameObject _destinationWorldGameObject = GameObject.Find(destinationWorldName);
            World _destinationWorld = _destinationWorldGameObject.GetComponent<World>();
            DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, _destinationWorld.fogDensity, duration);
            DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, _destinationWorld.fogColor, duration);
        }
        else
        {
            DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, density, duration);
            DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, color, duration);
        }

    }

    private void Start()
    {
        if (_e == null)
            _e = new FogChangeEvent();

        _e.AddListener(TweenFog);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_hasTriggered) { _e.Invoke(nav.next, fogColor, fogDensity); _hasTriggered = true; StartCoroutine(SetTimeout(10f, () => { _hasTriggered = false; })); }
    }

    // Utils
    IEnumerator SetTimeout(float time, TimeoutCallback Method)
    {
        yield return new WaitForSeconds(time);

        Method();
    }
}
