using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Scriptable Object Installers/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public EnemySpawner.Settings EnemySpawner;
    public GameInstaller.Settings GameInstaller;

    public override void InstallBindings()
    {
        Container.BindInstance(EnemySpawner).IfNotBound();
        
        Container.BindInstance(GameInstaller).IfNotBound();
    }
}