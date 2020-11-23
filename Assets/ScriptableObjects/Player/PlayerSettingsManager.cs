using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsManager", menuName = "ScriptableObjects/PlayerSettingsManager")]
public class PlayerSettingsManager : ScriptableObject
{
    public PlayerSettings freeMoveWithControls;
    public PlayerSettings freeMoveWithSound;
    public PlayerSettings moveAlongPathWithControls;
    public PlayerSettings moveAlongPathWithSound;
}
