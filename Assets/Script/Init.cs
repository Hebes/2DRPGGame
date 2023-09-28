using Core;

public class Init : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        InitGame initGame = new InitGame();
        initGame.Start();
    }
}
