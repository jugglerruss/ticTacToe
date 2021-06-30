using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSingle : Game
{
    [SerializeField] private Transform _playerPrefab1;
    [SerializeField] private Transform _playerPrefab2;
    private void Start()
    {
        GameType = "Single";

        GameObject player;
        player = InstantiatePlayer(true);
        PlayerSingle.My = player.GetComponent<PlayerSingle>();
        PlayerSingle.My.ItsMyTurn(false);
        PlayerSingle.My.NoFiguresDraw += Player_NoFiguresDraw;
        _ui.SetMyNickName(PlayerPrefs.GetString("NickName", "Player"));

        player = InstantiatePlayer(false);
        player.AddComponent<Bot>();
        PlayerSingle.Bot = player.GetComponent<PlayerSingle>();
        _ui.SetBotName(PlayerPrefs.GetString("DifficultyName", ""));

        _ui.SetScoreInfo(GameType);
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
            PlayerSingle.Bot.Victory();
        else
            PlayerSingle.Bot.Lose();
    }

}