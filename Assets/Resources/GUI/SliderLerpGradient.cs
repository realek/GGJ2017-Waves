using UnityEngine;
using UnityEngine.UI;

public class SliderLerpGradient : MonoBehaviour
{
    public Gradient x;

    private Slider attachedSlider;
    public Image targetImage;

    /// <summary>
    /// 'Normalizes' the current slider value.
    /// </summary>
    private float nrmAttachedSliderValue
    {
        get
        {
            if (attachedSlider.minValue < 0)
            {
                return attachedSlider.value + Mathf.Abs(attachedSlider.minValue);
            }
            return attachedSlider.value + attachedSlider.minValue;
        }
    }

    /// <summary>
    /// 'Normalizes' the max slider value.
    /// </summary>
    private float nrmAttachedSliderMaxValue
    {
        get
        {
            if (attachedSlider.minValue < 0)
            {
                return attachedSlider.maxValue + Mathf.Abs(attachedSlider.minValue);
            }
            return attachedSlider.maxValue + attachedSlider.minValue;
        }
    }

    void Awake ()
    {
        attachedSlider = GetComponent<Slider>();
        if (!targetImage)
            targetImage = attachedSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    void Update ()
    {
        targetImage.color = x.Evaluate(nrmAttachedSliderValue / nrmAttachedSliderMaxValue);
    }
}
