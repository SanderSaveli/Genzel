using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private LevelChanger changer;
    public int score;
    private void OnEnable()
    {
        PlayerPrefs.DeleteKey(changer.levelKey);
        if (PlayerPrefs.HasKey(changer.levelKey) == false || PlayerPrefs.GetInt(changer.levelKey) == 0)
        {
            PlayerPrefs.SetInt(changer.levelKey, 1);
            continueButton.onClick.RemoveAllListeners();
            continueButton.image.color = Color.gray;
        }
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt(changer.levelKey, 1);
        changer.FadeToLevel(1);
    }

    public void ContinueGame()
    {
        changer.FadeToLevel(PlayerPrefs.GetInt(changer.levelKey));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SpriteChangeSFX()
    {

    }
}
