using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ReactiveVFX : MonoBehaviour
{
    [Tooltip("Target VFX")]
    [SerializeField]
    VisualEffect vfx;

    [Tooltip("Target VFX Prop")]
    [SerializeField]
    string propName = "";

    [SerializeField]
    [Tooltip("Target VFX Float")]
    float number = 0f;

    [SerializeField]
    [Tooltip("Target VFX Int")]
    int intNumber = 0;

    [SerializeField]
    float min = 0.04f;

    [SerializeField]
    float max = 0.5f;

    Vector3 _vector = new Vector3(1, 1, 1);

    [SerializeField]
    public PropType propType = PropType.Float;
    public enum PropType
    {
        Float,
        Vector3,
        Int
    }

    [SerializeField]
    DataObject data;

    [Tooltip("Multiply mic volume by this")]
    public float amplitude;

    void ChangeProp()
    {
        // float prev = vfxOutput.GetFloat(prop);
        switch (propType)
        {
            case PropType.Float:
                float clamp = Mathf.Clamp(data.micVolumeNormalized * amplitude, 0, 1);
                number = clamp.Map(0, 1, min, max);
                vfx.SetFloat(propName, number);
                break;

            case PropType.Vector3:
                float value = data.micVolumeNormalized.Map(0, 1, min, max);
                Vector3 v = _vector * value;
                vfx.SetVector3(propName, v);
                break;

            case PropType.Int:
                float initialValue = Mathf.Clamp(data.micVolumeNormalized * amplitude, min, max);
                vfx.SetInt(propName, Mathf.RoundToInt(initialValue));
                break;

            default:
                break;
        }
    }

    void Update()
    {
        ChangeProp();
    }


}

public static class ExtensionMethods
{
    public static float Map(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
