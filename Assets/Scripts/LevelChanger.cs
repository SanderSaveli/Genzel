using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public readonly string levelKey = "lastCompleted";
    public Animator animator;
    private int levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        if(levelIndex >= SceneManager.sceneCount)
        {
            levelIndex = 0;
            PlayerPrefs.SetInt(levelKey, 2);
        }
        levelToLoad = levelIndex;
        animator.SetTrigger("Fade_Out");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}