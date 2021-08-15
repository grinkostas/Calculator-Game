using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ChangeDifficultButton : MonoBehaviour
{
    [SerializeField] private string _difficult;
    private Button _button;

    public UnityAction<ChangeDifficultButton> DifficultChanged;
    public string Difficult => _difficult;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        
        _button.onClick.AddListener(() => { DifficultChanged?.Invoke(this); });        
    }

    public void SetOpacity(float opacity)
    {
        Color color = _button.image.color;
        color.a = opacity;
        _button.image.color = color;
    }

}
