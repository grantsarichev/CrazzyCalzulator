using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CalculatorDataPlayerPrefsSave : CalculatorDataSave
{
    private readonly string _scoreboardDataKey = "scoreboard";
        
    public override UniTask Save(CalculatorData data)
    {
        PlayerPrefs.SetString(_scoreboardDataKey,data.ScoreboardState);
        return UniTask.CompletedTask;
    }

    public override UniTask<CalculatorData> Load()
    {
        if (PlayerPrefs.HasKey(_scoreboardDataKey))
        {
            var savedCalculatorData = new CalculatorData()
                { ScoreboardState = PlayerPrefs.GetString(_scoreboardDataKey) };

            return UniTask.FromResult(savedCalculatorData);
        }
        
        Debug.LogError($"PlayerPrefs not contains entry with {_scoreboardDataKey} key");

        throw new PlayerPrefsException($"PlayerPrefs not contains entry with {_scoreboardDataKey} key");
    }
}
