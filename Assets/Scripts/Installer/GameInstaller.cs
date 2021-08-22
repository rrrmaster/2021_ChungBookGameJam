using Zenject;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    [SerializeField]
    private TutorialView tutorialView;
    [SerializeField]
    private ToolTip toolTip;

    [SerializeField]
    private Setting setting;
    public override void InstallBindings()
    {
        Container.Bind<Setting>().FromInstance(setting).AsSingle();

        Container.Bind<GameView>().FromInstance(gameView).AsSingle();
        Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
        Container.Bind<GameModel>().AsSingle();


        Container.Bind<ShopView>().FromInstance(shopView).AsSingle();
        Container.BindInterfacesAndSelfTo<ShopPresenter>().AsSingle();
        Container.Bind<ShopModel>().AsSingle();

        Container.Bind<InventoryView>().FromInstance(inventoryView).AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryPresenter>().AsSingle();

        Container.Bind<CalendarView>().FromInstance(calendarView).AsSingle();
        Container.BindInterfacesAndSelfTo<CalendarPresenter>().AsSingle();

        Container.Bind<TutorialView>().FromInstance(tutorialView).AsSingle();
        Container.BindInterfacesAndSelfTo<TutorialPresenter>().AsSingle();
    }

    [System.Serializable]
    public class Setting
    {
        public Tilemap TileMap;
        public Tilemap isCropTileMap;
        public Tile washingTile;
        public Tile noWashingTile;
    }
}
