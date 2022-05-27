using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{

    public VideoClip introVideoLandscape;
    public VideoClip introVideoPortrait;
    public VideoPlayer introVideoPlayer;
    public TMP_Text tapToPlayText;
    public Image logo;
    bool tapped = false;
    bool startTimer = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    private void OnEnable()
    {
        //introVideoPlayer.clip = introVideoLandscape;
        introVideoPlayer.url = "C:\\Users\\ibrah\\Downloads\\Video\\Landscape.mp4";
        //if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        //{
        //    introVideoPlayer.clip = introVideoLandscape;
        //}else if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        //{
        //    introVideoPlayer.clip = introVideoPortrait;
        //}
        introVideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!tapped)
            {
                tapped = true;
                tapToPlayText.GetComponent<textFadeInNOut>().fadeOut();
                logo.GetComponent<Animator>().SetTrigger("animate");
                startTimer = true;
                logo.GetComponent<TweenPosition>().enabled = true;
                logo.GetComponent<TweenScale>().enabled = true;
            }
        }
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                SceneHandler.Instance.menuManager.registrationHandler.showRegistrationPanel();
            }
        }
    }

    public void fadeIntroVideo()
    {
        StartCoroutine(FadeImageToZeroAlpha(2));
    }

    public IEnumerator FadeImageToZeroAlpha(float t)
    {
        RawImage i = introVideoPlayer.GetComponent<RawImage>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}