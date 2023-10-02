using Core;
using RPGGame;
using Debug = UnityEngine.Debug;


public class Init : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Init∆Ù∂Ø¡À");
        InitGame initGame = new InitGame();
        initGame.Start();
    }
}
