using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 3)]
public class GameEvent : ScriptableObject
{
	/// <summary>
	/// The list of listeners that this event will notify if it is raised.
	/// </summary>
	private List<GameEventListener> listeners =
		new List<GameEventListener>();

	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised();
	}

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}