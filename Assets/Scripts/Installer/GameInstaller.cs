using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField]
    private GameView gameView;
    [SerializeField]
    private ShopView shopView;

    public override void InstallBindings()
    {
        Container.Bind<GameView>().FromInstance(gameView).AsSingle();
        Container.BindInterfacesTo<GamePresenter>().AsSingle();
        Container.Bind<GameModel>().AsSingle();


        Container.Bind<ShopView>().FromInstance(shopView).AsSingle();
        Container.BindInterfacesTo<ShopPresenter>().AsSingle();
        Container.BindInterfacesTo<ShopModel>().AsSingle();

        Debug.Log("aa");
    }
}
