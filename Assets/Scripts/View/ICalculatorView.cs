using System;

public interface ICalculatorView : IView
{
    ICalculatorViewPresenter CalculatorViewPresenter { get;}
    void SetScoreboardText(string scoreboardText);
    public string GetScoreboardText();
}
