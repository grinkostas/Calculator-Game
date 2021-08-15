using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultChanger : MonoBehaviour
{
    [SerializeField] private ChangeDifficultButton[] _buttons;
    [Range(0,1)] [SerializeField] private float _buttonsOpacity;

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.DifficultChanged += OnDifficultChanged;
        }
    }

    private void OnDifficultChanged(ChangeDifficultButton button)
    {
        PlayerPrefs.SetString(Equation.Difficult, button.Difficult);
        ChangeButtonsColor(button);
    }

    private void ChangeButtonsColor(ChangeDifficultButton clickedButton)
    {
        foreach (var button in _buttons)
        {
            button.SetOpacity(0);
        }
        clickedButton.SetOpacity(_buttonsOpacity);
    }
}
