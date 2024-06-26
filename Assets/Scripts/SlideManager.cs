using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public List<GameObject> texts = new List<GameObject>();

    public AudioClip slide1, slide2;
    public AudioSource source;
    public Image image;
    private int curentSlide = 0;
    private LevelChanger levelChanger;
    private void Start()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
        ShowNextSlide();
        source.clip = slide1;
        source.Play();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            ShowNextSlide();
            source.Stop();
            source.clip = slide2;
            source.Play();
        }
    }

    private void ShowNextSlide()
    {
        if (curentSlide >= sprites.Count)
        {
            NextLevel();
            return;
        }
        if (curentSlide != 0)
        {
            texts[curentSlide - 1].SetActive(false);
        }
        image.sprite = sprites[curentSlide];
        texts[curentSlide].SetActive(true);
        curentSlide++;
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt(levelChanger.levelKey, PlayerPrefs.GetInt(levelChanger.levelKey) + 1);
        levelChanger.FadeToLevel(PlayerPrefs.GetInt(levelChanger.levelKey));
        source.Stop();
    }
}
