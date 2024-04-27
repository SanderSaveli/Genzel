using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private GameField gameField; 
    public override void InstallBindings()
    {
        base.InstallBindings();
        Container.Bind<GameField>().To<GameField>().FromInstance(gameField).AsSingle();

    }
}
