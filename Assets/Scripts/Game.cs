using UnityEngine;
using UnityEngine.Advertisements;

public class Game : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Equation _equantion;
    [SerializeField] private EndGameMenu _menu;
    [SerializeField] private Menu _startMenu;
    [Space]
    [SerializeField] private AudioSource _correctSound;
    [SerializeField] private AudioSource _dieSound;
    [Space]
    [SerializeField] private float _timeMultiplayer;
    [SerializeField] private int _sessionsWithoutAd;
    private int _score = 0;

    private const string AdsPref = "SessionsWithoutsAd";
    private const string PlacementRewardedVideo = "Rewarded_Android";
    private const string PlacementVideo = "Interstitial_Android";
    //Ads
    private string _gameId = "4256799";
    private bool _testMode = false;

    private void Awake()
    {        
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, _testMode);
    }

    private void OnEnable()
    {
        _timer.TimeUp += EndGame;
        _equantion.CorrectAnswer += OnCorrectAnswer;
        _equantion.GameFinished += EndGame;
        _equantion.RestartGame += Restart;
        _equantion.NextStage += OnNextStage;
    }

    private void OnDisable()
    {
        _timer.TimeUp -= EndGame;
        _equantion.CorrectAnswer -= OnCorrectAnswer;
        _equantion.GameFinished -= EndGame;
        _equantion.RestartGame -= Restart;
        _equantion.NextStage -= OnNextStage;
    }
    private void Start()
    {
        int sessions = PlayerPrefs.GetInt(AdsPref);
        if (sessions >= _sessionsWithoutAd)
        {
            Advertisement.Show(PlacementVideo);
            sessions = 0;
        }
        else
        {
            sessions++;
        }
        PlayerPrefs.SetInt(AdsPref, sessions);

    }
    

    private void OnNextStage()
    {
        Time.timeScale *= _timeMultiplayer;
    }
   
    private void EndGame()
    {
        EndGame(false);
    }

    private void EndGame(bool isFinished = false)
    {
        _menu.Show(_score, _timer.WastedTime);
        Time.timeScale = 1;
        _timer.IsActive = false;

        if (isFinished)
        {
            _correctSound.Play();
        }
        else
        {
            _dieSound.Play();
        }
    }

    private void OnCorrectAnswer()
    {
        _score++;
        _timer.Reset();
        _correctSound.Play();
    }
    private void RewardForAd(string placementId, ShowResult showResult)
    {
        if (placementId == PlacementRewardedVideo)
        {
            if (showResult == ShowResult.Finished || showResult == ShowResult.Skipped)
            {
                _menu.Hide();
                _timer.Reset(false);
                PlayerPrefs.SetInt(AdsPref, 0);
            }
        }
    }
    public void Restart()
    {
        _startMenu.Show();
        _timer.Reset(false);
        _menu.Hide();
    }

    public void Resume()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(PlacementRewardedVideo); 
        }        
    }
     
    public void OnUnityAdsReady(string placementId)
    {}

    public void OnUnityAdsDidError(string message)
    {    }
    public void OnUnityAdsDidStart(string placementId)
    {}

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        RewardForAd(placementId, showResult);
    }

   

}
