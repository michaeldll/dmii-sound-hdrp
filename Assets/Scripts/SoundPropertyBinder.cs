using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPropertyBinder : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float min = 70f;

    [SerializeField]
    float max = 80f;

    [SerializeField]
    Vector3 vector = new Vector3(1, 1, 1);

    [SerializeField]
    public PropType propType = PropType.Scale;
    public enum PropType
    {
        Scale,
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
            case PropType.Scale:
                float value = Mathf.Clamp((data.micVolumeNormalized * amplitude), 0, 1).Map(0, 1, min, max);
                Vector3 v = vector * value;
                target.localScale = v;
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
