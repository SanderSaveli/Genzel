using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private readonly string levelKey = "lastCompleted";
    [SerializeField] private Button continueButton;
    public int score;
    private void OnEnable()
    {
        PlayerPrefs.DeleteKey(levelKey);
        if (PlayerPrefs.HasKey(levelKey) == false || PlayerPrefs.GetInt(levelKey) == 0)
        {
            PlayerPrefs.SetInt(levelKey, 0);
            continueButton.onClick.RemoveAllListeners();
            continueButton.image.color = Color.gray;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(levelKey));
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
