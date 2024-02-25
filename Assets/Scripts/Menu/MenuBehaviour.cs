using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuBehaviour : MonoBehaviour {

    public int delay = 2;

    public Canvas menu;
    public Canvas quitMenu;
    public Canvas helpSplash;
    public Canvas optionsMenu;
    //public Canvas controlCanvas;

    public Button startText;
    public Button optionText;
    public Button helpText;
    public Button exitText;

    public AudioClip[] otherClip;

    private AudioSource audio;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    IEnumerator Start() {

        menu = menu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        helpSplash = helpSplash.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        //controlCanvas = controlCanvas.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        optionText = optionText.GetComponent<Button>();
        helpText = helpText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        audio = GetComponent<AudioSource>();

        //controlCanvas.enabled = false;
        helpSplash.enabled = false;
        quitMenu.enabled = false;
        optionsMenu.enabled = false;

        int rand = Random.Range(0, 4);
        audio.clip = otherClip[rand];
        audio.Play();

        yield return new WaitForSeconds(5);
    }

    void Update()
    {
        if (!audio.isPlaying || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.JoystickButton3))
        {
            int rand = Random.Range(0, 4);
            audio.clip = otherClip[rand];
            audio.Play();
        }
    }

    public void OptionsMenu() {
        quitMenu.enabled = false;
        helpSplash.enabled = false;
        startText.enabled = false;
        optionText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        optionsMenu.enabled = true;
    }

    public void ExitPress() {
        quitMenu.enabled = true;
        helpSplash.enabled = false;
        startText.enabled = false;
        optionText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        optionsMenu.enabled = false;
    }

    public void SplashClose() {
        quitMenu.enabled = false;
        helpSplash.enabled = false;
        startText.enabled = true;
        optionText.enabled = true;
        helpText.enabled = true;
        exitText.enabled = true;
        optionsMenu.enabled = false;
    }

    public void HelpSplash() {
        quitMenu.enabled = false;
        helpSplash.enabled = true;
        startText.enabled = false;
        optionText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        optionsMenu.enabled = false;
    }

    public void StartGame()
    {
        StartCoroutine(StartLevel());
    }

    public IEnumerator StartLevel()
    {
        //controlCanvas.enabled = true;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("_Scenes/Map01");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
