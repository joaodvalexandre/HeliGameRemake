using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HoverOptions : MonoBehaviour {

    public Canvas resolutionPopup;
    public Canvas musicPopup;
    public Canvas soundPopup;

    public Button resolutionBox;
    public Button musicBox;
    public Button soundBox;
    public float time = 2f;

    public float timeSet;

    void Start() {
        resolutionPopup = resolutionPopup.GetComponent<Canvas>();
        musicPopup = musicPopup.GetComponent<Canvas>();
        resolutionBox = resolutionBox.GetComponent<Button>();
        musicBox = musicBox.GetComponent<Button>();
        soundBox = soundBox.GetComponent<Button>();

        resolutionPopup.enabled = false;
        musicPopup.enabled = false;
        soundPopup.enabled = false;

        timeSet = time;
    }

    void Update()
    {
        if (resolutionPopup.enabled || musicPopup.enabled || soundPopup.enabled) {
            timeSet -= 0.0025f;

            if (timeSet <= 0)
            {
                CloseSplash();
            }
        }
    }

    public void OpenResolutionHelp()
    {
        resolutionPopup.enabled = true;
        musicPopup.enabled = false;
        soundPopup.enabled = false;
        timeSet = time;
    }

    public void OpenMusicHelp()
    {
        resolutionPopup.enabled = false;
        musicPopup.enabled = true;
        soundPopup.enabled = false;
        timeSet = time;
    }

    public void OpenSoundHelp()
    {
        resolutionPopup.enabled = false;
        musicPopup.enabled = false;
        soundPopup.enabled = true;
        timeSet = time;
    }

    void CloseSplash()
    {
        resolutionPopup.enabled = false;
        musicPopup.enabled = false;
        soundPopup.enabled = false;
        timeSet = time;
    }
}
