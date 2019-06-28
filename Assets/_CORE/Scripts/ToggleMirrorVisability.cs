using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Hide and revial mirror using callable animation
/// </summary>
public class ToggleMirrorVisability : MonoBehaviour
{
    const string aniBoolName = "IsVisable";             //!< Name of the animation used in animation controller
    [SerializeField] Animator AnimationController;

    [ContextMenu("Toggle")]
    public void Toggle()
    {
        bool current = AnimationController.GetBool(aniBoolName);
        AnimationController.SetBool(aniBoolName, !current);
    }
}
