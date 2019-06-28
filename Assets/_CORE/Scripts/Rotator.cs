using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates an object around the y axis over time
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float speed = 1;               //!< Speed that the rotation occors
    [SerializeField] float stepAngle = 90;          //!< The angle in degrees that is rotated each rotation

    public bool isRotating { get; private set; } = false;   //!< Is the object currently mid rotation 

    /// <summary>
    /// Rotate object one step over time
    /// </summary>
    public void Rotate()
    {
        if (!isRotating)
            StartCoroutine(RotateOverTime(stepAngle));
    }

    /// <summary>
    /// Rotate object over time using a specific angle
    /// </summary>
    public void Rotate(float angle)
    {
        if (!isRotating)
            StartCoroutine(RotateOverTime(angle));
    }


    private IEnumerator RotateOverTime(float angle)
    {
        var startAngle = transform.rotation;
        var endAngle = Quaternion.Euler(0, angle, 0) * transform.rotation;

        float startTime = Time.time;
        float lerptime = 0;

        isRotating = true;
        audioSource.Play();

        while (lerptime < 1)
        {
            lerptime = (Time.time - startTime) * speed;
            transform.rotation = Quaternion.Lerp(startAngle, endAngle, lerptime);

            yield return 0;
        }

        isRotating = false;
        audioSource.Stop();
    }
}
