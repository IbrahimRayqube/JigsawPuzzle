using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationHandler : MonoBehaviour
{

    public GameObject registrationPopup;
    public GameObject keyboard;
    public TMPro.TMP_Text nameText, phoneText, emailText;
    public bool consentAccepted = false;
    public Toggle consent;
    public Button submitButton;
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
        //registrationAnimator.SetBool("Visible", true);
        registrationPopup.GetComponent<TweenPosition>().enabled = true;
        StartCoroutine(showKeyboardWithDelay());
    }

    IEnumerator showKeyboardWithDelay()
    {
        yield return new WaitForSeconds(1f);
        keyboard.GetComponent<TweenPosition>().enabled = true;
    }

    public void onClickSubmit()
    {
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
        SceneHandler.Instance.menuManager.introHandler.fadeIntroVideo();
        yield return new WaitForSeconds(2);
        SceneHandler.Instance.startGameWithDelay();
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

}
