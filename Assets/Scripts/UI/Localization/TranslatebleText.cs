using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TranslatebleText : MonoBehaviour
{
    [SerializeField] private string _translateKey;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    private void Start()
    {
        UpdateText();
    }
    private void OnEnable()
    {
        Translator.LanguageChanged += UpdateText;
        if (Translator.IsReady)
        {
            UpdateText();
        }
    }

    private void OnDisable()
    {
        Translator.LanguageChanged -= UpdateText;
        
    }

    private void UpdateText()
    { 
        string text = Translator.GetTranslatedText(_translateKey);
      
        if (text != null)
        {
            _text.text = text;
        }
    }
}
