using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField]
    private GameView gameView;
    [SerializeField]
    private ShopView shopView;
    [SerializeField]
    private InventoryView inventoryView;

    [SerializeField]
    private CalendarView calendarView;

    public override void InstallBindings()
    {
        Container.Bind<GameView>().FromInstance(gameView).AsSingle();
        Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
        Container.Bind<GameModel>().AsSingle();


        Container.Bind<ShopView>().FromInstance(shopView).AsSingle();
        Container.BindInterfacesTo<ShopPresenter>().AsSingle();
        Container.Bind<ShopModel>().AsSingle();

        Container.Bind<InventoryView>().FromInstance(inventoryView).AsSingle();
        Container.BindInterfacesTo<InventoryPresenter>().AsSingle();

        Container.Bind<CalendarView>().FromInstance(calendarView).AsSingle();
        Container.BindInterfacesTo<CalendarPresenter>().AsSingle();

    }
}
