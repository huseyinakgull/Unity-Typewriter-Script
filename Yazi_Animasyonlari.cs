using System.Collections;
using UnityEngine;
using TMPro;

public class TextTyping : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public AudioClip typeSound;
    public bool fadeOut = false;
    private string fullText;
    private string currentText = "";
    private AudioSource audioSource;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textMeshPro = GetComponent<TextMeshProUGUI>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the object.");
            return;
        }

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the object.");
            return;
        }

        Debug.Log("AudioSource component found: " + audioSource.name);

        fullText = textMeshPro.text;
        textMeshPro.text = "";

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        Color originalColor = textMeshPro.color;

        foreach (char c in fullText)
        {
            currentText += c;
            textMeshPro.text = currentText;

            if (typeSound != null)
                audioSource.PlayOneShot(typeSound);

            yield return new WaitForSeconds(typingSpeed);
        }

        if (fadeOut)
        {
            while (textMeshPro.color.a > 0.0f)
            {
                textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, textMeshPro.color.a - 0.1f);
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}
