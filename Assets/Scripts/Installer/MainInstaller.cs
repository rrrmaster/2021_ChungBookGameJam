using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller<MainInstaller>
{
    [SerializeField]
    private MainView mainView;
    [SerializeField]
    private OptionView optionView;

    public override void InstallBindings()
    {

        Container.Bind<MainPresenter>().AsSingle();
        Container.Bind<OptionView>().FromInstance(optionView).AsSingle();
        Container.Bind<MainView>().FromInstance(mainView).AsSingle();
    }
}
