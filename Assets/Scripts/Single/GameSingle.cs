using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSingle : Game
{
    [SerializeField] private Transform _playerPrefab1;
    [SerializeField] private Transform _playerPrefab2;
    private void Start()
    {
        GameObject player;
        player = InstantiatePlayer(true);
        PlayerSingle.My = player.GetComponent<PlayerSingle>();
        PlayerSingle.My.ItsMyTurn(false);
        _players[0] = PlayerSingle.My;

        player = InstantiatePlayer(false);
        player.AddComponent<Bot>();
        PlayerSingle.II = player.GetComponent<PlayerSingle>();
        _players[1] = PlayerSingle.II;
    }
    public void LeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(2);
    }
    private GameObject InstantiatePlayer(bool isFirst)
    {
        var playerPrefab = isFirst ? _playerPrefab1 : _playerPrefab2;
        return Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation).gameObject;
    }
    protected override void AnimateBot(bool win)
    {
        if(win)
            _players[1].Victory();
        else
            _players[1].Lose();
    }
}