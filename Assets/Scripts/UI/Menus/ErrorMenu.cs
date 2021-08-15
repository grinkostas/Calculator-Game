using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMenu : Menu
{
    [SerializeField] private Text _text;
    public void Show(string text)
    {
        base.Show();
        _text.text = text;
    }
}
