using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private readonly string levelKey = "lastCompleted";
    [SerializeField] private Button continueButton;
    public Text counter;
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

    private void Update()
    {
        
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

    public void ChangeSound()
    {

    }

    public void ChangeMusic() 
    {
        int increment = 1;
        int totalCompleted = PlayerPrefs.GetInt(levelKey);
        PlayerPrefs.SetInt(levelKey, increment + totalCompleted);
        score++;
    }
}
