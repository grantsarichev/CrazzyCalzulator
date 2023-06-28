using System;
using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CalculatorView : MonoBehaviour, ICalculatorView
{
    [SerializeField] private TMP_InputField _scoreboardText;
    [SerializeField] private Button _resultButton;
    [SerializeField] private TMP_Text _operationsHistoryText;
    
    private StringBuilder _historyTextCache = new("");

    public ICalculatorViewPresenter CalculatorViewPresenter { get; private set; }

    private IDisposable _disposable;
    
    [Inject]
    public void Init(ICalculatorViewPresenter calculatorViewPresenter)
    {
        CalculatorViewPresenter = calculatorViewPresenter;
    }

    private void OnEnable()
    {
        _disposable = _resultButton.onClick.AsObservable()
            .Subscribe(_=> ResultButtonHandler().Forget());
    }

    async UniTask ResultButtonHandler()
    {
        var scoreboardResultText = await CalculatorViewPresenter.ResultClickHandler(_scoreboardText.text);
        SetOperationsHistoryText(scoreboardResultText);
        SetScoreboardText("");
    }

    public void SetScoreboardText(string scoreboardText) => _scoreboardText.text = scoreboardText;

    public string GetScoreboardText() => _scoreboardText.text;

    public void SetOperationsHistoryText(string scoreboardText)
    {
        _historyTextCache.Insert(0,scoreboardText);
        _historyTextCache.Insert(scoreboardText.Length,"\n");
        _operationsHistoryText.text = _historyTextCache.ToString();
    }

    private void OnDisable()
    {
        _disposable?.Dispose();
    }
}
