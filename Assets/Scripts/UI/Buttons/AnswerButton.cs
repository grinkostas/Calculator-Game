using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    [SerializeField] private Equation _equation;
    [SerializeField] private Text _text;

    private Answer _answer;
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void ChangeAnswer(Answer answer)
    {
        _answer = answer;
        _text.text = answer.answer.ToString();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => _equation.Answer(_answer));
    }

    private int[] TwoSum(int[] numbers, int targetNumber)
    {
        int[] positions = new int[2];
        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = i; j < numbers.Length; j++)
            {
                if (numbers[i] + numbers[j] == targetNumber)
                {
                    positions[0] = i;
                    positions[1] = j;
                }
            }
        }
        return positions;

    }



}
