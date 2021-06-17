using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{

    [SerializeField]
    private UnityEvent OnWin;
    [SerializeField]
    private UnityEvent OnLose;

    private static Game instance;
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Game>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(Game).Name;
                    instance = obj.AddComponent<Game>();
                }
            }
            return instance;
        }
    }
    #region Methods

    protected virtual void Awake()
    {
        if (instance == null)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    #endregion
    public bool CheckWin(Figure currentFigure)
    {
        if (Board.My.CheckLines() != 0)
        {
            if (currentFigure.photonView.IsMine)
            {
                OnWin.AddListener(Player.My.Victory);
                OnWin.Invoke();
                OnWin.RemoveListener(Player.My.Victory);
            }
            else
            {
                OnLose.AddListener(Player.My.Lose);
                OnLose.Invoke();
                OnWin.RemoveListener(Player.My.Lose);
            }
        }
        return false;
    }
}
