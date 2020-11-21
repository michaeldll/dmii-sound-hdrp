using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScene : MonoBehaviour
{
    public enum GizmoType { Never, SelectedOnly, Always };
    public GizmoType showScreenSceneRegion;
    public Color color = new Color(0, 0, 0, 0.5f);
    public float screenSceneSize = 5;

    private void OnDrawGizmos()
    {
        if (showScreenSceneRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showScreenSceneRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    void DrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, screenSceneSize);
    }
}
