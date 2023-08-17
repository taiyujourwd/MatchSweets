using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //甜品類型
    public enum SweetsType
    {
        EMPTY,
        NORMAL,
        BARRIER,
        ROW_CLEAR,
        COLUMN_CLEAR,
        RAINBOWCANDY,
        COUNT //標記類型
    }

    public Dictionary<SweetsType, GameObject> sweetPrefabDict;

    [System.Serializable]
    public struct SweetPrefab
    {
        public SweetsType type;
        public GameObject prefab;
    }

    public SweetPrefab[] sweetPrefabs;

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

    //甜品數組
    private GameSweet[,] sweets;

    private void Awake()
    {
        //單例init
        _instance = this;
    }

    void Start()
    {
        //字典實例化
        sweetPrefabDict = new Dictionary<SweetsType, GameObject>();
        for (int i = 0; i < sweetPrefabs.Length; i++)
        {
            if (!sweetPrefabDict.ContainsKey(sweetPrefabs[i].type))
            {
                sweetPrefabDict.Add(sweetPrefabs[i].type, sweetPrefabs[i].prefab);
            }
        }

        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                GameObject chocolate = Instantiate(gridPrefab, CorrectPosition(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);
            }
        }

        sweets = new GameSweet[xColumn, yRow];
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                GameObject newSweet = Instantiate(sweetPrefabDict[SweetsType.NORMAL], Vector3.zero, Quaternion.identity);
                newSweet.transform.SetParent(transform);

                sweets[x, y] = newSweet.GetComponent<GameSweet>();
                sweets[x, y].Init(x, y, this, SweetsType.NORMAL);

                if (sweets[x, y].CanMove())
                {
                    sweets[x, y].MovedComponent.Move(x, y);
                }

                if (sweets[x, y].CanColor())
                {
                    sweets[x, y].ColoredComponent.SetColor((ColorSweet.ColorType)Random.Range(0, sweets[x, y].ColoredComponent.NumColors));
                }
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
