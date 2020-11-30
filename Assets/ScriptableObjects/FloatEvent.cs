using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityFloatEvent : UnityEvent<float>
{
}

[CreateAssetMenu(fileName = "FloatEvent", menuName = "ScriptableObjects/FloatEvent")]
public class FloatEvent : ScriptableObject
{
    public UnityFloatEvent e = new UnityFloatEvent();
}