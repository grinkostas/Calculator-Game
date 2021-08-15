using System.Collections;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

[RequireComponent(typeof(Text))]
public class Equation : MonoBehaviour
{
    [Header("Answer Buttons")]
    [SerializeField] private AnswerButton _answerButton1;
    [SerializeField] private AnswerButton _answerButton2;
    [SerializeField] private AnswerButton _answerButton3;
    [Space]

    [SerializeField] private Difficult<TextAsset> _difficultyEquations;
    
    [SerializeField] private string _additionalText;

    private TextAsset _equationsFile;
    private List<Exercise> _allEquations;
    private List<Exercise> _equations;

    List<EquationType> _types;
    private Text _text;
    

    private AnswerButton[] _answerButtons => new AnswerButton[3] { _answerButton1, _answerButton2, _answerButton3 };   

    public UnityAction CorrectAnswer;
    public UnityAction<bool> GameFinished;
    public UnityAction RestartGame;
    public UnityAction NextStage;

    public static readonly string Difficult = "Difficult";
    private void Awake()
    {
        _allEquations = new List<Exercise>();
        _text = GetComponent<Text>();
    }

    
    private void GenerateExercises()
    {
        string difficulty = PlayerPrefs.GetString(Difficult);   
        _equationsFile = _difficultyEquations.GetDifficult(difficulty);
        string[] equations = _equationsFile.text.Split(
            new[] { "\r\n", "\r", "\n" },
            System.StringSplitOptions.None
        );
        for(int i = 0; i < equations.Length; i++)
        {
            Exercise exercise = GenerateExercise(equations[i]);
            if (exercise != null)
            {
                _allEquations.Add(exercise);
            }
        }
    }
    private Exercise GenerateExercise(string item)
    {
        Exercise exercise = null;
        int correct;
        if (item.Contains("/"))
        {
            string[] tempS = item.Split('/');
            correct = System.Int32.Parse(tempS[0]) / System.Int32.Parse(tempS[1]);
        }
        else
        {
            DataTable dt = new DataTable();
            correct = (int)dt.Compute(item, "");
        }
        
        Answer[] answers = new Answer[3];
        answers[0] = new Answer(correct, true); //correct answer
        int randCorrectAnswerPosition = Random.Range(0, 3);
        int range = answers[0].answer / 2;
        for (int i = 1; i < 3; i++)
        {
            int tempAnswer = Random.Range(1, range);
            if (Random.Range(0, 2) == 0)
            {
                tempAnswer *= -1;
            }
            tempAnswer += answers[0].answer;
            answers[i] = new Answer(tempAnswer);
        }
        Answer temp = answers[randCorrectAnswerPosition];
        answers[randCorrectAnswerPosition] = answers[0];
        answers[0] = temp;
        EquationType type;
        if (item.Contains("+"))
            type = EquationType.Addition;
        else if (item.Contains("-"))
            type = EquationType.Subtraction;
        else if (item.Contains("*"))
            type = EquationType.Multiplication;
        else
            type = EquationType.Division;

        exercise = new Exercise(item, answers, type);
        return exercise;
    }
    private void GetEquations(List<EquationType> types)
    {
        List<Exercise> equations = new List<Exercise>();
        foreach (var item in _allEquations)
        {
            if (types.Contains(item.type))
            {
                equations.Add(item);
            }
        }
        _equations = equations;
    }

   
    private Exercise NextEquation()
    {
        if (_equations.Count <= 1)
        {
            GetEquations(_types);
            NextStage?.Invoke();
        }
        int rand = Random.Range(0, _equations.Count);
        Exercise exercise = _equations[rand];
        _equations.Remove(exercise);
        return exercise;
    }
    private void SetEquation(Exercise exercise)
    {
        _text.text = exercise.text + _additionalText;
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].ChangeAnswer(exercise.answers[i]);
        }
    }
    public void Init(List<EquationType> types)
    {
        _types = types;
        _allEquations.Clear();
        GenerateExercises();
        GetEquations(_types);
        SetEquation(NextEquation());
    }

    public void Answer(Answer answer)
    {
        if (answer.isCorrect)
        {
            SetEquation(NextEquation());
            CorrectAnswer?.Invoke();
        }
        else
        {
            GameFinished?.Invoke(false);
        }
    }
}
[System.Serializable]
public enum EquationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

[System.Serializable]
public class Difficult<T>
{
    public T Easy;
    public T Medium;
    public T Hard;

    public T GetDifficult(string difficult)
    {
        T result;
        difficult = difficult.ToLower();
        switch (difficult)
        {
            case "medium":
            case "1":
                result = Medium;
                break;
            case "hard":
            case "2":
                result = Hard;
                break;
            default:
                result = Easy;
                break;
        }
        return result;
    }
}
[System.Serializable]
public class Answer
{
    public int answer;
    public bool isCorrect;

    public Answer(int ans, bool correct = false)
    {
        answer = ans;
        isCorrect = correct;
    }
}
[System.Serializable]
public class Exercise
{
    public string text;
    public Answer answers1;
    public Answer answers2;
    public Answer answers3;
    public EquationType type;
    public Answer[] answers => new Answer[3] { answers1, answers2, answers3 };

    public Exercise(string _text, Answer[] _answers, EquationType _type)
    {
        text = _text;
        answers1 = _answers[0];
        answers2 = _answers[1];
        answers3 = _answers[2];
        type = _type;
    }
}
