using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance;

    private float duration;
    private bool _isGameOver;
    public bool IsGameOver => _isGameOver;

    private void Awake()
    {
        var gameObj = GameObject.FindGameObjectWithTag("GameController");
        Instance = gameObj.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_isGameOver) return;

        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            duration = 0;
        }
    }
}