using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        // Au démarrage du menu, définir Meilleur score selon les PlayerPrefs

        if (PlayerPrefs.HasKey("HIGHSCORE"))
        {
            highscoreText.text = PlayerPrefs.GetInt("HIGHSCORE").ToString("# ###");
        } else
        {
            highscoreText.text = "0";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void ToggleHelpPanel(bool visible)
    {
        helpPanel.SetActive(visible);
    }
}
