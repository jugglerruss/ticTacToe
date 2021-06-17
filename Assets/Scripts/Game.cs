using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent OnWin;
    [SerializeField]
    protected UnityEvent OnLose;

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
    void Awake()
    {
        if (instance == null)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public virtual bool CheckWin(Figure currentFigure)
    {
        if (Board.My.CheckLines() != 0)
        {
            if (CheckOwner(currentFigure.transform))
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

    protected virtual bool CheckOwner(Transform figureTransform)
    {
        return figureTransform.parent == Player.My.transform;
    }
    #endregion
}
