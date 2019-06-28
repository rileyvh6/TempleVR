using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Laser hit event callled whenever a laser comes incontect of object
/// </summary>
public interface ILaserTarget
{
    void OnLaserHit(Laser sender);
}
