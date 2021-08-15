using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public Toggle Toggle;
    public EquationType Type;
    public int Number;

    private const string IsOnPref = "IsToggleOn";

    private string _idPref;

    private void Start()
    {
        _idPref = IsOnPref + Number.ToString();
        int isOn = PlayerPrefs.GetInt(_idPref);
        if (isOn == 1)
        {
            Toggle.isOn = false;
        }
    }

    private void OnDisable()
    {
        int result;
        if (Toggle.isOn == true)
        {
            result = 0;
        }
        else
        {
            result = 1;
        }
        PlayerPrefs.SetInt(_idPref, result);
    }

}
