using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Puzzle goal endpoint
/// </summary>
public class Goal : MonoBehaviour, ILaserTarget
{
    const float colourSensitivity = 0.1f;               //!< Colour accuracy between the required colour and the lasers final colour 
    const float detectionTimeDelay = 1f;                //!< Time that the laser must be incontact with the goal in order to pass the puzzle

    private float successHitTime = 0;                   //!< Time that the laser must be incontact with goal for a sucsessful goal hit
    private Laser lastHit;                              //!< Laser laser object that hit this goal

    public bool isColourSensitive = false;              //!< Does the laser need to be a specific colour to beat the puzzle.
    public Color requiredLaserColour = Color.white;     //!< Colour required to pass puzzle.
    private bool GoalActivated = false;                 //!< Used to disable behavoir once activated


    public void OnLaserHit(Laser sender)
    {
        if (GoalActivated)
            return;

        if (isColourSensitive && ColorDelta(sender.endColour, requiredLaserColour) > colourSensitivity)
            return;

        if (lastHit == sender) {
            if (Time.time >= successHitTime)
                GoalHit();

            return;
        }

        lastHit = sender;
        successHitTime = Time.time + detectionTimeDelay;
    }


    private void GoalHit() {
        GoalActivated = true;
        LevelManager.CompletedPuzzle();
        this.enabled = false;
    }


    static float ColorDelta(Color c1, Color c2){
        return  Mathf.Abs(c1.r - c2.r) +
                Mathf.Abs(c1.g - c2.g) +
                Mathf.Abs(c1.b - c2.b);
    }

}
