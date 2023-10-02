using Core;
using RPGGame;
using Debug = UnityEngine.Debug;


public class Init : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Init������");
        InitGame initGame = new InitGame();
        initGame.Start();
    }
}
