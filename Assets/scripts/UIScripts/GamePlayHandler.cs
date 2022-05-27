using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GamePlayHandler : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text gameEndText;
    public GameObject leaderBoard;
    public Slider timerSlider;
    public bool startTime = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime && SceneHandler.Instance.isGamePlay)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer).ToString();
            timerSlider.value = timer;
            if (timer <= 0)
            {
                timerText.gameObject.SetActive(false);
                timerSlider.gameObject.SetActive(false);
                SceneHandler.Instance.timeUp();
            }
        }
    }

    public void startTimer(float time)
    {
        timer = time;
        startTime = true;
        timerSlider.minValue = 0;
        timerSlider.maxValue = time;
        timerSlider.value = timer;
    }
}
