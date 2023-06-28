using Cysharp.Threading.Tasks;

public abstract class CalculatorDataSave
{
    public abstract UniTask Save(CalculatorData data);
    public abstract UniTask<CalculatorData> Load();
}
