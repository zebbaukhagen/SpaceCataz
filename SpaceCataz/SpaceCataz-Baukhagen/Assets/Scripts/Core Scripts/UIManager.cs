//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UIManager handles the various UI elements and keeping them updated
    /// </summary>
    
    [SerializeField] Image healthBarFill;
    [SerializeField] Image livesNumber;
    [SerializeField] GameObject sequencePanel;
    [SerializeField] GameObject lossPanel;

    [SerializeField] List<Image> scoreCounter;
    [SerializeField] List<Sprite> listOfNumericSprites = new();

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "End Game")
        {
            // if the scene is not the end scene, set the appropriate elements
            GameObject temp = GameObject.Find("HealthBarFill");
            healthBarFill = temp.GetComponent<Image>();
            temp = GameObject.Find("LivesNumber");
            livesNumber = temp.GetComponent<Image>();
        }
    }
    public void SetLivesCounter(int numberOfLives)
    {
        livesNumber.sprite = listOfNumericSprites[numberOfLives];
    }

    public void SetHealthBarFill(float normalizedHealthValue)
    {
        healthBarFill.fillAmount = normalizedHealthValue;
    }

    public void SetScoreCounter(int score)
    {
        // convert the score integer to a string
        string scoreString = score.ToString();
        // pad it with '0's on the left until it is length 4
        scoreString = scoreString.PadLeft(4, '0');
        // turn that into a char array
        char[] scoreCharArray = scoreString.ToCharArray();

        for (int i = 0; i < scoreCharArray.Length; i++)
        {
            // go through and set each sprite of the score counter to each given char
            scoreCounter[i].sprite = listOfNumericSprites[int.Parse(scoreCharArray[i].ToString())];
        }
    }

    public void ActivateSequencePanel()
    {
        sequencePanel.SetActive(true);
    }

    public void ActivateLossPanel()
    {
        lossPanel.SetActive(true);
    }
}
