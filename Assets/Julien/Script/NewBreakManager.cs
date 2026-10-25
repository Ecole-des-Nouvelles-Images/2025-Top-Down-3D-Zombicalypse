using Julien.Script;
using UnityEngine;

public class NewBreakManager : MonoBehaviourSingleton<NewBreakManager>
{
    [SerializeField] private bool _inBreak;
    [SerializeField] private float _maxBreakDuration;
    [SerializeField] private float _currentBreakDuration;

    public void StartBreak()
    {
        _currentBreakDuration = _maxBreakDuration;
        _inBreak = true;
        Debug.Log("Start Break");
    }

    private void Update()
    {
        if (_inBreak)
        {
            _currentBreakDuration -= Time.deltaTime;
            if (_currentBreakDuration <= 0)
            {
                _inBreak = false;
                EndBreak();
            }
        }
    }

    private void EndBreak()
    {
        NewRoundManager.Instance.StartRound();
    }

    private void SpawnLargage()
    {

    }

    public void SetNight()
    {

    }

    public void SetDay()
    {

    }
}
