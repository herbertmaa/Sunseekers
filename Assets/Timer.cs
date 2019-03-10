using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Text text;
    public int remainingTime;
    public int timeToFinish = 300;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime = (timeToFinish - (int)Time.time) / 60 + 1;
        text.text = remainingTime + " DAYS";
        if (remainingTime <= 0)
        {
            SceneManager.LoadScene("WinScene");
            timeToFinish = (int)Time.time;
        }
    }

}
