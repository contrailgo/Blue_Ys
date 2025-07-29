using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    public Image bloodImage;
    public float fadeSpeed = 2f;

    private float alpha = 0f;
    private bool isFlashing = false;
    private Color originalColor;

    void Start()
    {
        if (bloodImage != null)
            originalColor = bloodImage.color;
    }

    public void Flash()
    {
        if (bloodImage == null) return;

        alpha = 0.6f;
        isFlashing = true;
    }

    void Update()
    {
        if (!isFlashing || bloodImage == null) return;

        alpha -= Time.deltaTime * fadeSpeed;

        if (alpha <= 0f)
        {
            alpha = 0f;
            isFlashing = false;
        }

        originalColor.a = alpha;
        bloodImage.color = originalColor;
    }
}


