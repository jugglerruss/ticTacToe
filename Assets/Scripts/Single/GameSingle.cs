using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSingle : Game
{
    [SerializeField] private Transform _playerPrefab1;
    [SerializeField] private Transform _playerPrefab2;
    [SerializeField] private Camera _camera;
    private void Start()
    {
        GameType = "Single";
        var playerId = PlayerPrefs.GetInt("playerId", 2);
        if (playerId == 1)
            playerId = 2;
        else
            playerId = 1;
        PlayerPrefs.SetInt("playerId", playerId);
        GameObject player;
        var isFirst = playerId == 1 ? true : false;
        player = InstantiatePlayer(isFirst);
        PlayerSingle.My = player.GetComponent<PlayerSingle>();
        PutCameraToPosition();
        PlayerSingle.My.ItsMyTurn(false);
        PlayerSingle.My.NoFiguresDraw += Player_NoFiguresDraw;
        _ui.SetMyNickName(PlayerPrefs.GetString("NickName", "Player"));

        player = InstantiatePlayer(!isFirst);
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

    private void PutCameraToPosition()
    {
        var cameraPos = Player.My.GetCameraPosition();
        _camera.transform.position = cameraPos.position - Player.My.transform.position;
        _camera.transform.rotation = cameraPos.rotation;
    }
}