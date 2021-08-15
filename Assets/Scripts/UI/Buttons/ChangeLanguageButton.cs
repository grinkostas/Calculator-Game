using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLanguageButton : MonoBehaviour
{
    [SerializeField] private Language _language;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeLanguage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeLanguage);
    }
    private void ChangeLanguage()
    {
        Translator.SetLanguage(_language);
    }
}
