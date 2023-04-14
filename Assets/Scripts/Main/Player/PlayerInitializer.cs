using Mirror;

public class PlayerInitializer : NetworkBehaviour
{
    private void Start()
    {
        if (!isLocalPlayer) return;

        LocalPlayerReference.SetPlayer(transform);
        GameStarter.StartGame();
    }
}
