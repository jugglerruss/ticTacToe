using UnityEngine;
using UnityEngine.Events;

public class GameOnline : Game
{
    private void Start()
    {
        _players = FindObjectsOfType<Player>();
    }
}
