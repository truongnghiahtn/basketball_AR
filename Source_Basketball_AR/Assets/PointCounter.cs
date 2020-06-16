using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<Text>().text = "0";
        highScoreText.GetComponent<Text>().text = PlayerPrefs.GetInt("highScore").ToString();
    }

    void OnTriggerEnter() //if ball hits basket collider
    {
        int currentScore = int.Parse(scoreText.GetComponent<Text>().text) + 1; //add 1 to the score
        int highScore = PlayerPrefs.GetInt("highScore");
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("highScore", currentScore);
            highScoreText.GetComponent<Text>().text = currentScore.ToString();
        }
        scoreText.GetComponent<Text>().text = currentScore.ToString();
    }
}
