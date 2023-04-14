using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class GameStateProcessor : InputProcessor<bool>
{
    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    #if UNITY_EDITOR
    static GameStateProcessor() => Initialize();
    #endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() => InputSystem.RegisterProcessor<GameStateProcessor>(nameof(DesiredGameState));
    public override bool Process(bool value, InputControl control) => DesiredGameState;
}