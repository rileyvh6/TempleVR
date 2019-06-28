using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotate all child gameobjects containing the Rotator script
/// </summary>
public class RotateChildren : MonoBehaviour
{
    private Rotator[] childRotators;

    public void Awake()
    {
        if  (transform.childCount < 1)
            Debug.LogError(string.Format("Rotate child script attached to {0} has no childrent attached.", this.name));

        childRotators = GetComponentsInChildren<Rotator>();
    }

    public void RotateAll()
    {
        foreach (Rotator rot in childRotators)
            if (rot.isRotating){
                Debug.Log("Rotation called while group is still rotateing. Aborted new rotation call");
                return;
            }

        foreach (Rotator rot in childRotators)
            rot.Rotate();
    }
}
