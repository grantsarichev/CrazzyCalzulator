using Cysharp.Threading.Tasks;
using UniRx;

public interface ICalculatorViewPresenter
{
   ICalculatorView CalculatorView { get; }
   UniTask<string> ResultClickHandler(string scoreboardText);
}
