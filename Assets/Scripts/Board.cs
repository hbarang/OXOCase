using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    bool[,] boardArray;
    int boardElementCount = 4;
    int screenWidth, screenHeight;
    float elementSize;
    float elementOffset;
    [SerializeField]
    GameObject buttonPrefab;

    public static Board Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        elementSize = Mathf.Min((float)screenWidth, (float)screenHeight) / (float)boardElementCount;
        elementOffset = elementSize / 2;

        boardArray = new bool[boardElementCount, boardElementCount];
        SetUpBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUpBoard()
    {
        float xCounter = elementOffset;
        float yCounter = elementOffset;
        GameObject button;
        RectTransform buttonTransform;
        for (int indexY = 0; indexY < boardElementCount; indexY++)
        {
            for (int indexX = 0; indexX < boardElementCount; indexX++)
            {
                button = Instantiate(buttonPrefab);
                button.GetComponent<BoardElement>().SetIndices(indexX, indexY);
                button.transform.SetParent(transform);
                buttonTransform = button.GetComponent<RectTransform>();
                buttonTransform.sizeDelta = new Vector2(elementSize, elementSize); ;
                buttonTransform.anchoredPosition = new Vector3(xCounter, -yCounter, 0);
                xCounter = indexX == boardElementCount - 1 ? elementOffset : xCounter + elementOffset * 2;
            }
            yCounter += elementOffset * 2;
        }
    }
}
