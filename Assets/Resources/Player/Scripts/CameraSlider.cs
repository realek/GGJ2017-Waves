using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraSlider : MonoBehaviour
{
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;

    void Awake ()
    {
        UpdateValues();
    }

    public void UpdateValues ()
    {
        Vector3 position = new Vector3(sliderX.value, sliderY.value, sliderZ.value);
        transform.position = position;
    }
}
