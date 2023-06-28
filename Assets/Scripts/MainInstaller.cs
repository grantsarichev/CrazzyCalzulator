using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICalculatorView>().To<CalculatorView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ICalculatorViewPresenter>().To<CalculatorViewPresenter>().FromNewComponentSibling().AsSingle();
        Container.Bind<CalculationModule>().AsSingle();
        Container.Bind<CalculatorDataSave>().To<CalculatorDataPlayerPrefsSave>().AsSingle();

    }
}