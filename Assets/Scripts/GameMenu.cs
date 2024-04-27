using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject nextLevelButton;
    public Button MoveButton;


    public void ShowWin()
    {
        nextLevelButton.SetActive(true);

    }

    public void ShowLose()
    {
        restartButton.SetActive(true);
    }

    public void NextLevel()
    {
        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartMove()
    {
        MoveButton.onClick.RemoveAllListeners();
    }
}
