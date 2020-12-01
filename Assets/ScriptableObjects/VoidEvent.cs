using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityVoidEvent : UnityEvent
{
}

[CreateAssetMenu(fileName = "VoidEvent", menuName = "ScriptableObjects/VoidEvent")]
public class VoidEvent : ScriptableObject
{
    public UnityVoidEvent e = new UnityVoidEvent();
}