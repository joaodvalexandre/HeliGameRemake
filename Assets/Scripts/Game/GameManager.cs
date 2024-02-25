using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioClip[] otherClip;
    public Canvas restartScreen;
    public Canvas ui;
    public Canvas menu;
    public Canvas quitMenu;
    public Canvas optionsMenu;
    public Text score;
    public Text currentScore;
    public GameObject obstacle;
    public Vector3 spawnValues;
    public float timer = 10f;
    public float scorePerSecond = 1f;

    private float points = 0f;
    private bool spawnDisable = false;
    private AudioSource audio;
    private float timerFixed;

    IEnumerator Start()
    {
        menu = menu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        restartScreen = restartScreen.GetComponent<Canvas>();
        ui = ui.GetComponent<Canvas>();
        score = score.GetComponent<Text>();
        currentScore = currentScore.GetComponent<Text>();
        Application.targetFrameRate = 60;

        audio = GetComponent<AudioSource>();
        int rand = Random.Range(0, 4);
        restartScreen.enabled = false;
        audio.clip = otherClip[rand];
        audio.Play();
        timerFixed = timer;

        quitMenu.enabled = false;
        optionsMenu.enabled = false;
        menu.enabled = false;

        yield return new WaitForSeconds(5);
    }

    void Update()
    {
        float pointsShown = (Mathf.Round(points * 1000)) / 1000.0f;

        if (timer <= 0)
        {
            timer = timerFixed;
        }
        currentScore.text = "TIME: " + pointsShown;
        if (GameObject.Find("Player") == null)
        {
            restartScreen.enabled = true;
            ui.enabled = false;
            spawnDisable = true;
            score.text = "YOUR TIME: "+ pointsShown;
            if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.JoystickButton1))
            {
                SceneManager.LoadScene("_Scenes/Map01");
            }
        }

        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton7))
        {
            Application.Quit();
        }

        if (!audio.isPlaying || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.JoystickButton3))
        {
            int rand = Random.Range(0, 4);
            audio.clip = otherClip[rand];
            audio.Play();
        }

        if (!spawnDisable)
        {
            while (timer == timerFixed)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);
                Instantiate(obstacle, spawnPosition, spawnRotation);
                timer -= Time.deltaTime;
            }
            timer -= Time.deltaTime;
            points += Time.deltaTime*scorePerSecond;
        }
    }

    public void MenuOpen()
    {
        menu.enabled = true;
        quitMenu.enabled = false;
        optionsMenu.enabled = false;
    }

    public void OptionsMenu()
    {
        quitMenu.enabled = false;
        optionsMenu.enabled = true;
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        optionsMenu.enabled = false;
    }

    public void SplashClose()
    {
        quitMenu.enabled = false;
        optionsMenu.enabled = false;
    }

    public void ExitMenu()
    {
        menu.enabled = false;
        quitMenu.enabled = false;
        optionsMenu.enabled = false;
    }
}
