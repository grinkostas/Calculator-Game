using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGame : MonoBehaviour
{
    [SerializeField] private Equation _equation;
    [SerializeField] private Menu _menu;
    [SerializeField] private ErrorMenu _error;

    private List<EquationType> _types;
    private Button _button;
    private void Awake()
    {
        _types = new List<EquationType>();
        _types.Add(EquationType.Addition);
        _types.Add(EquationType.Division);
        _types.Add(EquationType.Multiplication);
        _types.Add(EquationType.Subtraction);
        _button = GetComponent<Button>();

    }

    private void OnEnable()
    {
        _error.Hide();
        _button.onClick.AddListener(GameStart);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(GameStart);
    }


    private void GameStart()
    {
        if (_types.Count == 0)
        {
            _error.Show(Translator.GetTranslatedText("Error1")); ;
            return;
        }
        _equation.Init(_types);
        _menu.Hide();
    }


    public void TongleSwitch(Selector selector)
    {
        if (selector.Toggle.isOn == false)
        {
            if (_types.Contains(selector.Type) == true)
            {
                _types.Remove(selector.Type);
            }
        }

        if (selector.Toggle.isOn == true)
        {
            if (_types.Contains(selector.Type) == false)
            {
                _types.Add(selector.Type);
            }
        }
    }


}
