using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject nextLevelButton;
    public Button MoveButton;
    private LevelChanger levelChanger;
    private void Start()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
    }

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
        PlayerPrefs.SetInt(levelChanger.levelKey, PlayerPrefs.GetInt(levelChanger.levelKey) + 1);
        levelChanger.FadeToLevel(PlayerPrefs.GetInt(levelChanger.levelKey));
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
