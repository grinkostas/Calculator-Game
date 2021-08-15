using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _answerTime;
    [SerializeField] private Slider _slider;

    private float _wastedTime = 0;

    public UnityAction TimeUp;
    public bool IsActive { get; set; }
    public float WastedTime { get; set; }
    private void Start()
    {
        IsActive = false;
    }

    private void Update()
    {
        if (IsActive)
        {
            Actualize();
        }

        if (_wastedTime >= _answerTime)
        {
            TimeUp?.Invoke();
        }
    }

    private void Actualize()
    {
        _wastedTime += Time.deltaTime;
        WastedTime += Time.deltaTime;
        _slider.value = 1 - (_wastedTime / _answerTime);
    }

    public void Reset(bool isActive = true)
    {
        _wastedTime = 0;
        Actualize();
        IsActive = isActive;
    }


}
