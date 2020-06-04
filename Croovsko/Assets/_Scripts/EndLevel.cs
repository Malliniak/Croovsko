using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private LevelState nextLevelState;

    private void Awake()
    {
        AssetLoader.GetAssetFile(out nextLevelState, $"LvL{Int32.Parse(SceneManager.GetActiveScene().name) + 1}State");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LeftRightController>())
        {
            if (nextLevelState != null) nextLevelState.Unlocked = true;
            SceneManager.LoadScene(1);
        }
    }
}
