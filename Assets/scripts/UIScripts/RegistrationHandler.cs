using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationHandler : MonoBehaviour
{

    public GameObject registrationPopup;
    public GameObject keyboard;
    public TMPro.TMP_Text nameText, phoneText, emailText;
    public bool consentAccepted = false;
    public Toggle consent;
    public Button submitButton;
    //public Dropdown orientationDropDown, puzzeTimeDropDown;
    public TMPro.TMP_Text passwordText;
    public TMP_Dropdown orientation;
    public TMP_Dropdown timeDropDown;
    public GameObject settingsPopup;
    public TMP_Text consentText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showRegistrationPanel()
    {
        registrationPopup.gameObject.SetActive(true);
        //registrationAnimator.SetBool("Visible", true);
        registrationPopup.GetComponent<TweenPosition>().enabled = true;
        registrationPopup.GetComponent<TweenPosition>().PlayForward();
        StartCoroutine(showKeyboardWithDelay());
    }

    IEnumerator showKeyboardWithDelay()
    {
        yield return new WaitForSeconds(1f);
        keyboard.GetComponent<TweenPosition>().enabled = true;
        keyboard.GetComponent<TweenPosition>().PlayForward();
    }

    public void onClickSubmit()
    {
        SceneHandler.Instance.puzzleBoard.gameObject.SetActive(true);
        SceneHandler.Instance.puzzleBoard.setToCorrectPosition();
        SceneHandler.Instance.playerData.name = nameText.text;
        SceneHandler.Instance.playerData.phone = phoneText.text;
        SceneHandler.Instance.playerData.email = emailText.text;

        StartCoroutine(showPuzzleWithDelay());
    }

    IEnumerator showPuzzleWithDelay()
    {
        keyboard.GetComponent<TweenPosition>().PlayReverse();
        yield return new WaitForSeconds(1);
        registrationPopup.GetComponent<TweenPosition>().PlayReverse();
        yield return new WaitForSeconds(1);
        if (SceneHandler.Instance.isPortrait)
        {
            SceneHandler.Instance.menuManagerPortrait.introHandler.fadeIntroVideo();
        }
        else
        {
            SceneHandler.Instance.menuManager.introHandler.fadeIntroVideo();
        }
        yield return new WaitForSeconds(2);
        SceneHandler.Instance.startGameWithDelay();
        registrationPopup.GetComponent<TweenPosition>().enabled = false;
        //registrationPopup.gameObject.SetActive(false);
    }

    public void onClickInputField(TMPro.TMP_Text selectedInput)
    {
        keyboard.GetComponent<KeyBoardManager>().textBox = selectedInput;
    }

    public void onToggleCheckBox()
    {
        if (consent.isOn)
        {
            consentAccepted = true;
            submitButton.interactable = true;
        }
        else
        {
            consentAccepted = false;
            submitButton.interactable = false;
        }
    }

    public void onClickSubmitSettings()
    {
        if (passwordText.text.Equals("RayQube"))
        {
            PlayerPrefs.SetInt("Orientation", orientation.value);
            PlayerPrefs.SetInt("Time", timeDropDown.value);
            settingsPopup.SetActive(false);
        }
    }

    public void onPressSettings()
    {
        settingsPopup.gameObject.SetActive(true);
    }

}
