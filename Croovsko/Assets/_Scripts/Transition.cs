using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    
    private Animator _transitionAnimator;
    private int _sceneToLoad;

    private void Awake()
    {
        _transitionAnimator = GetComponent<Animator>();
    }
    
    public void Closed()
    {
        LoadScene();
    }
    
    public void LoadScene(int index)
    {
        _transitionAnimator.SetTrigger("CloseScene");
        _sceneToLoad = index;
    }

    public void ReloadScene()
    {
        _transitionAnimator.SetTrigger("CloseScene");
        _sceneToLoad = SceneManager.GetActiveScene().buildIndex;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
