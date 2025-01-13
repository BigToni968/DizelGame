using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIBlock : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    private float _max;

    public void Init(float max)
    {
        _max = max;
        _slider.maxValue = max;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
        _text.SetText($"{_max} / {value}");
    }
}