using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //單例
    private static GameManager _instance;
    public GameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    //大網格行列數
    public int xColumn;
    public int yRow;

    public GameObject gridPrefab;

    private void Awake()
    {
        //單例init
        _instance = this;
    }

    void Start()
    {
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                GameObject chocolate = Instantiate(gridPrefab, CorrectPosition(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);
            }
        }
    }

    void Update()
    {

    }

    public Vector3 CorrectPosition(int x, int y)
    {
        //實際需要實例化巧克力塊的X位置＝GameManager位置的X座標-大網格長度的一半+行列對應的X座標
        //實際需要實例化巧克力塊的Y位置＝GameManager位置的Y座標-大網格長度的一半+行列對應的Y座標
        return new Vector3(transform.position.x - xColumn / 2f + x, transform.position.y + yRow / 2f - y);
    }
}
