using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class AmountChoiseView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _count;
    [Space]
    [SerializeField] private GameObject _menu;
    private Action<int> _callback;
    private UnityAction<float> _sliderChange;

    private void OnEnable ()
    {
        _sliderChange += UpdateCount;
        _slider.onValueChanged.AddListener(_sliderChange);
        UpdateCount(_slider.value);
    }

    private void OnDisable ()
    {
        _slider.onValueChanged.RemoveListener(_sliderChange);
        _sliderChange -= UpdateCount;
    }

    public void Choise (string title, int max, Action<int> callback)
    {
        _title.text = title;
        _slider.value = 1;
        _slider.maxValue = max;
        _callback = callback;
        
        _menu.SetActive(true);
    }

    public void OnConfirm () 
    { 
        _callback((int)_slider.value);
        _menu.SetActive(false);
    }

    private void UpdateCount (float value) => _count.text = value.ToString();
}
