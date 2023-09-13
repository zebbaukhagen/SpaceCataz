//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    /// <summary>
    /// MenuController handles the various button methods
    /// </summary>
    
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] List<GameObject> panels;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void OnClickStartButton()
    {
        sceneLoader.LoadNextScene();
    }

    public void OnClickHelpButton()
    {
        if (panels[1].activeSelf)
        {
            panels[1].SetActive(false);
        }

        panels[0].SetActive(true);
    }   
    
    public void OnClickCreditsButton()
    {
        if (panels[0].activeSelf)
        {
            panels[0].SetActive(false);
        }

        panels[1].SetActive(true);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    public void OnClickMainMenuButton()
    {
        sceneLoader.LoadSpecificScene(0);
    }
}
