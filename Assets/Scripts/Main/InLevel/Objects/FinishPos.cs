using UnityEngine;

public class FinishPos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerFinish player))
            player.OnGameFinished();
    }
}
