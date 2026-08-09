using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSliderCtl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueTxt;
    [SerializeField] private ASkey asKey;

    private Slider _slider;
    private int _previousValue;
    private int _currentValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _previousValue = (int)_slider.value;
        _currentValue = _previousValue;
        UpdateVolumeSlider();
        UpdateValueText();
    }

    private void Update()
    {
        _currentValue = (int)_slider.value;
        if (_currentValue != _previousValue)
        {
            UpdateValueText();
            UpdateVolume();
            _previousValue = _currentValue;
        }
    }

    private void UpdateValueText()
    {
        valueTxt.text = _currentValue.ToString();
    }

    private void UpdateVolume()
    {
        SoundMgr.instance.SetVolume(asKey, _slider.value/10f);
    }

    private void UpdateVolumeSlider()
    {
        _slider.value = SoundMgr.instance.GetVolume(asKey) * 10f;
    }
}
