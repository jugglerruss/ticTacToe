using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSingle : Game
{
    public  void LeftRoom()
    {
        SceneManager.LoadScene(0);
    }
}