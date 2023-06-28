using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CalculatorViewPresenter : MonoBehaviour,ICalculatorViewPresenter
{
    public ICalculatorView CalculatorView { get; private set; }
    private CalculationModule _calculationModule;
    private CalculatorDataSave _calculatorDataSave;
    
    [Inject]
    public void Init(ICalculatorView calculatorView,CalculationModule calculationModule , CalculatorDataSave calculatorDataSave)
    {
        CalculatorView = calculatorView;
        _calculationModule = calculationModule;
        _calculatorDataSave = calculatorDataSave;
    }

    private void Start()
    {
        TryRestoreScoreboardState().Forget();
    }

    private async UniTaskVoid TryRestoreScoreboardState()
    {
        try
        {
            var scoreboardData = await _calculatorDataSave.Load();
            CalculatorView.SetScoreboardText(scoreboardData.ScoreboardState);
        }
        catch
        {
            CalculatorView.SetScoreboardText("");
        }
    }

    public  async UniTask<string> ResultClickHandler(string scoreboardText)
    {
        try
        {
            var result = await _calculationModule.Calculate(scoreboardText);
            return result.ToString();
        }
        catch
        {
            return "ERROR";
        }
    }

    private void OnApplicationQuit()
    {
        _calculatorDataSave.Save(new CalculatorData() { ScoreboardState = CalculatorView.GetScoreboardText() }).Forget();
    }
}
