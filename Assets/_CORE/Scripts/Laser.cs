using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Laser line that can bounce and interect with the environment
/// </summary>
[ExecuteInEditMode]
public class Laser : MonoBehaviour
{
    const string ReflectTag = "Mirror";             //!< transform tag used to determin if the laser can bounce off the surface
    const int bounceLimit = 20;                     //!< Maximum iterations the laser bounces will be calculated 
    const float maxDistance = 500f;                 //!< Max raycast search distance
    const float passThroughBias = 1;                //!< Units to increase the last bounce point after hitting colour change object in order to prevent multiple raycast hits

    LineRenderer[] lines;                           //!< Line render game objects
    public Color endColour { get; private set; }    //!< Colour of the laser line at its final location 


    public void Awake() {
        lines = GetComponentsInChildren<LineRenderer>(true);
    }

    private void OnValidate() {
        lines = GetComponentsInChildren<LineRenderer>(true);
    }


    public void Update() {
        BounceLaser();
    }

    /// <summary>
    /// Calculate trajectory of laser as well as register hit events
    /// </summary>
    public void BounceLaser()
    {
        int lineSegment = 0;
        var points = new List<Vector3> { transform.position };
        Vector3 bounceDirection = transform.forward * -1;
        endColour = Color.white;

        for (int b = 0; b < bounceLimit; b++)
        {
            Ray ray = new Ray(points[points.Count - 1], bounceDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                points.Add(hit.point);

                // Change laser colour if hit object contains lasercolourchange script
                var colourchange = hit.transform.GetComponent<LaserColourChange>();
                if (colourchange != null)
                {
                    SetLine(lines[lineSegment], endColour, points.ToArray());
                    lineSegment++;
                    points = new List<Vector3> { hit.point + bounceDirection * 0.2f, hit.point + bounceDirection * passThroughBias };
                    endColour = colourchange.color;
                }
                else {
#if UNITY_EDITOR
                    if (Application.isPlaying)
#endif
                    {
                        // Send hit event to laserTarget scripts
                        var target = hit.transform.GetComponent<ILaserTarget>();
                        if (target != null)
                            target.OnLaserHit(this);
                    }


                    if (hit.transform.tag == ReflectTag) {
                        // Reflect laser and continute bounce iterations
                        bounceDirection = Vector3.Reflect(bounceDirection, hit.normal);
                    }
                    else {
                        break;  // Stop laser bounce loop if hit object is not tagged as mirror.
                    }

                }
            }
            // Raycast didnt hit anything, add line point far into the distance and stop bounce loop.
            else {
                points.Add(bounceDirection * maxDistance);
                break;
            }
        }

        // Update final linerender
        SetLine(lines[lineSegment], endColour, points.ToArray());

        // Disable unused linerender segments
        for (int i = lineSegment + 1; i < lines.Length; i++) {
            lines[i].enabled = false;
        }

    }

    /// <summary>
    /// Set postions and colour of linerender as well as activating the visability
    /// </summary>
    private void SetLine(LineRenderer line, Color c, Vector3[] points)
    {
        line.colorGradient = new Gradient
        {
            colorKeys = new GradientColorKey[2] {
                new GradientColorKey(c, 0),
                new GradientColorKey(c, 1) }
        };

        line.positionCount = points.Length;
        line.SetPositions(points);
        line.enabled = true;
    }
}
