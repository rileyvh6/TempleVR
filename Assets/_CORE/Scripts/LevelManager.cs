using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads new puzzles and stages (scenes)
/// </summary>
public class LevelManager : MonoBehaviour
{
    private static LevelManager Singleton;

    private Animation ani;                                      //!< Puzzle switching animation
    [SerializeField] Transform LevelPuzzles;                    //!< Parent gameobject that contains each puzzle.
    public int CurrentPuzzleIndex { get; private set; } = 0;
    public Animation SceneLoader;                                //!< Physical button that the player can presss to advence to the nxt stage


    private void Awake()
    {
        Singleton = this;
        ani = GetComponent<Animation>();
    }

    private void OnDestroy()
    {
        Singleton = null;
    }

    public static void CompletedPuzzle()
    {
        if (Singleton.CurrentPuzzleIndex >= Singleton.LevelPuzzles.childCount - 1)
        {
            LoadNextStage();
        }
        else
        {
            Singleton.CurrentPuzzleIndex++;
            Singleton.ani.Play("PuzzleSwap", PlayMode.StopAll);
        }
    }


    private void LoadNextPuzzle()
    {
        for (int i = 0; i < LevelPuzzles.childCount; i++)
            LevelPuzzles.GetChild(i).gameObject.SetActive(i == CurrentPuzzleIndex);
    }

    private static void LoadNextStage()
    {
        if (Singleton.SceneLoader != null)
        {
            Singleton.HideAll();
            Singleton.SceneLoader.gameObject.SetActive(true);
            Singleton.SceneLoader.Play("SceneTransition");
        }
        else
        {
            GameEnd();
        }
    }

    private void HideAll()
    {
        foreach(Transform c in transform)
            c.gameObject.SetActive(false);
    }

    private static void GameEnd()
    {
        Debug.Log("Nothing implemented on game end.");
    }



    private void Update()
    {
#if UNITY_EDITOR
        // Development cheats
        if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Skipping level");
                CompletedPuzzle();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Skipping stage");
                LoadNextStage();
            }
#endif
    }

    [ContextMenu("Cycle puzzle")]
    private void EditorCyclePuzzle()
    {
        CurrentPuzzleIndex = CurrentPuzzleIndex >= LevelPuzzles.childCount - 1 ? 0 : CurrentPuzzleIndex + 1;
        LoadNextPuzzle();
    }

}
