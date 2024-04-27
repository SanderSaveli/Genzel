using UnityEngine;
using UnityEngine.UI;

public class SpriteState : MonoBehaviour
{
    [SerializeField] private Sprite spriteState1;
    [SerializeField] private Sprite spriteState2;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SpriteChange()
    {
        if (image.sprite == spriteState1)
        {
            image.sprite = spriteState2;
        }
        else
        {
            image.sprite = spriteState1;
        }
    }
}
