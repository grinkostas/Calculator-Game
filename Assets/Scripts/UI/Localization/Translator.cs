using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum Language
{ 
    Russian,
    English,
    Ukrainian, 
    Polish, 
    German,
    French,
    Spanish

}

public class Translator:MonoBehaviour
{
    [SerializeField] private TextAsset _xmlLocalization;

    private static Dictionary<string, Dictionary<string, string>> _localization;  
    private const string LanguagePref = "Language";
    private static Language _currentLanguage;

    public static bool IsReady = false;
    public static event UnityAction LanguageChanged;

    private void Awake()
    {
        _localization = new Dictionary<string, Dictionary<string, string>>();
        _localization = LoadLocalization();
        _currentLanguage = GetLanguage();
        SetLanguage(_currentLanguage);
        IsReady = true;
    }

    private Language GetLanguage()
    {
        if (PlayerPrefs.HasKey(LanguagePref) == false)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                PlayerPrefs.SetInt(LanguagePref, (int)Language.Russian);
            else if (Application.systemLanguage == SystemLanguage.Polish)
                PlayerPrefs.SetInt(LanguagePref, (int)Language.Polish);
            else if (Application.systemLanguage == SystemLanguage.Ukrainian)
                PlayerPrefs.SetInt(LanguagePref, (int)Language.Ukrainian);
            else if (Application.systemLanguage == SystemLanguage.French)
                PlayerPrefs.SetInt(LanguagePref, (int)Language.French);
            else if (Application.systemLanguage == SystemLanguage.Spanish)
                PlayerPrefs.SetInt(LanguagePref, (int)Language.Spanish);
            else if (Application.systemLanguage == SystemLanguage.German)
                PlayerPrefs.SetInt(LanguagePref, (int)Language.German);
            else
                PlayerPrefs.SetInt(LanguagePref, (int)Language.English);
        }
        return (Language)PlayerPrefs.GetInt(LanguagePref);
    }
    private Dictionary<string, Dictionary<string, string>> LoadLocalization()
    {
        var localization = new Dictionary<string, Dictionary<string, string>>();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(_xmlLocalization.text);
        
        foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string textKey = key.Attributes["name"].Value;
            var translates = new Dictionary<string, string>();
            foreach (XmlNode translate in key["Translates"].ChildNodes)
            {
                translates.Add(translate.Name, translate.InnerText);
            }
            localization.Add(textKey, translates);
        }
        return localization;

    }

    public static void SetLanguage(Language language)
    {
        if (language == _currentLanguage)        
            return;
        
        PlayerPrefs.SetInt(LanguagePref, (int)language);
        _currentLanguage = language;
        LanguageChanged?.Invoke();
    }

    public static string GetTranslatedText(string textKey)
    {
        string result = null;
        
        if (_localization.ContainsKey(textKey))
        {
            if (_localization[textKey].ContainsKey(_currentLanguage.ToString()))
            {
                result = _localization[textKey][_currentLanguage.ToString()];
            }
        }
        return result;
    }

   
}
