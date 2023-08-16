using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameSweet : MonoBehaviour
{
    private int x;
    public int X
    {
        get { return x; }

        set
        {
            if (CanMove())
            {
                x = value;
            }
        }
    }

    private int y;
    public int Y
    {
        get { return y; }
        set
        {
            if (CanMove())
            {
                y = value;
            }
        }
    }

    private GameManager.SweetsType type;
    public GameManager.SweetsType Type
    {
        get { return type; }
    }

    private MovedSweet movedComponent;
    public MovedSweet MovedComponent
    {
        get { return movedComponent; }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        movedComponent = GetComponent<MovedSweet>();
    }
    public bool CanMove()
    {
        return movedComponent != null;
    }

    [HideInInspector]
    public GameManager gameManager;

    public void Init(int _x, int _y, GameManager _gameManager, GameManager.SweetsType _type)
    {
        X = _x;
        Y = _y;
        gameManager = _gameManager;
        type = _type;
    }
}
