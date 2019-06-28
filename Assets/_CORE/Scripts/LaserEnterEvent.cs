using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Detects when the laser first hits a gameobject
/// </summary>
public class LaserEnterEvent : MonoBehaviour, ILaserTarget
{
    public bool OneTimeTrigger = false;         //!< Ignore all successive hits after the first
    bool hasActivated = false;                  //!< Has this object been hit by the laser, use for one time trigger
    [SerializeField] UnityEvent OnTrigger;      //!< UnityEvent function to call apon laser enter
    Laser frameHit;                             //!< Laser object that was detected in the current frame
    Laser lastHit;                              //!< Last known framehit

    public void OnLaserHit(Laser sender)
    {
        if (OneTimeTrigger)
        {
            if (hasActivated)
                return;
            hasActivated = true;
        }

        frameHit = sender;
    }

    public void LateUpdate()
    {
        if (frameHit == null)
        {
            lastHit = null;
            return;
        }

        if (lastHit == null)
            OnTrigger.Invoke();

        lastHit = frameHit;
        frameHit = null;
    }
}