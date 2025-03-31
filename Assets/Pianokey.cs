using UnityEngine;
using UnityEngine.UI;

public class PianoKey : MonoBehaviour
{
    public AudioClip noteSound; // Âm thanh của phím đàn
    private AudioSource audioSource;
    public KeyCode assignedKey; // Phím trên bàn phím để chơi nốt nhạc

    private SpriteRenderer spriteRenderer; // Nếu là Sprite 2D
    private Image buttonImage; // Nếu là UI Button
    private Color originalColor; // Màu gốc của phím đàn
    public Color pressedColor = Color.gray; // Màu khi bấm phím

    void Start()
    {
        // Lấy SpriteRenderer (nếu là phím 2D)
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        // Lấy Image (nếu là UI Button)
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }

        // Thêm AudioSource nếu chưa có
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = noteSound;
        audioSource.playOnAwake = false;

        // Thêm sự kiện khi nhấn chuột (nếu là UI Button)
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayNote);
        }
    }

    void Update()
    {
        // Khi nhấn phím trên bàn phím, phát nhạc & đổi màu
        if (Input.GetKeyDown(assignedKey))
        {
            PlayNote();
            ChangeColor(pressedColor);
        }

        // Khi nhả phím, đổi lại màu gốc
        if (Input.GetKeyUp(assignedKey))
        {
            ChangeColor(originalColor);
        }
    }

    void PlayNote()
    {
        audioSource.Play();
    }

    void ChangeColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
        if (buttonImage != null)
        {
            buttonImage.color = newColor;
        }
    }
}
