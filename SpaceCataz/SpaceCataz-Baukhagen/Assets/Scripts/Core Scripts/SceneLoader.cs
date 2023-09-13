//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// SceneLoader handles loading the scenes in order
    /// </summary>
    
    private int currentSceneIndex;
    public int CurrentSceneIndex => currentSceneIndex;

    private int nextSceneIndex;

    private void Start()
    {
        // on Start, make sure that time is moving, get the active scene, calculate index and next index and
        // then call NewSceneSetup from the GameManager
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
        Debug.Log("Current Scene is: " + currentSceneIndex);
        Debug.Log("Next scene is: " + nextSceneIndex);
        if (currentSceneIndex != 0)
        {
            GameManager.Instance.NewSceneSetup();
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadSpecificScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

}

