﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    int screenWidth, screenHeight;
    float elementSize;
    float elementOffset;
    [SerializeField]
    GameObject buttonPrefab;
    List<GameObject> buttons;
    int connectedElementCount = 0;
    int _matchCount = 0;
    public int MatchCount
    {
        get
        {
            return _matchCount;
        }
        set
        {
            _matchCount = value;
            if (MatchCountChangedEvent != null)
            {
                MatchCountChangedEvent(value);
            }

        }
    }

    public BoardElement[,] boardArray;
    public int boardElementCount = 4;
    public static Board Instance;
    public delegate void OnMatchCountChange(int matches);
    public event OnMatchCountChange MatchCountChangedEvent;

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
        buttons = new List<GameObject>();
    }

    void Start()
    {
        SetUpBoard();
    }


    public void SetUpBoard()
    {
        if (buttons.Count != 0)
        {
            foreach (var item in buttons)
            {
                Destroy(item);
            }
        }

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        elementSize = Mathf.Min((float)screenWidth, (float)screenHeight) / (float)boardElementCount;
        elementOffset = elementSize / 2;
        MatchCount = 0;

        boardArray = new BoardElement[boardElementCount, boardElementCount];
        float xCounter = elementOffset;
        float yCounter = elementOffset;
        GameObject button;
        RectTransform buttonTransform;
        for (int indexY = 0; indexY < boardElementCount; indexY++)
        {
            for (int indexX = 0; indexX < boardElementCount; indexX++)
            {
                button = Instantiate(buttonPrefab);
                buttons.Add(button);
                button.GetComponent<BoardElement>().SetIndices(indexX, indexY);
                boardArray[indexX, indexY] = button.GetComponent<BoardElement>();

                button.transform.SetParent(transform);
                buttonTransform = button.GetComponent<RectTransform>();
                buttonTransform.sizeDelta = new Vector2(elementSize, elementSize); ;
                buttonTransform.anchoredPosition = new Vector3(xCounter, -yCounter, 0);

                xCounter = indexX == boardElementCount - 1 ? elementOffset : xCounter + elementOffset * 2;
            }
            yCounter += elementOffset * 2;
        }
    }

    public void CheckAddedElement(int indiceX, int indiceY)
    {

        bool[,] isVisited = new bool[boardElementCount, boardElementCount];
        connectedElementCount = 0;
        CheckNeighbours(isVisited, indiceX, indiceY);
        if (connectedElementCount > 2)
        {
            bool[,] newIsVisited = new bool[boardElementCount, boardElementCount];
            MatchCount += 1;
            ResetNeighbours(newIsVisited, indiceX, indiceY);
        }

    }

    void CheckNeighbours(bool[,] isVisited, int indiceX, int indiceY)
    {
        if (indiceX < 0 || indiceX >= boardElementCount || indiceY < 0 || indiceY >= boardElementCount || isVisited[indiceX, indiceY] || !boardArray[indiceX, indiceY].clicked)
        {
            return;
        }
        isVisited[indiceX, indiceY] = true;
        connectedElementCount += 1;
        CheckNeighbours(isVisited, indiceX + 1, indiceY);
        CheckNeighbours(isVisited, indiceX - 1, indiceY);
        CheckNeighbours(isVisited, indiceX, indiceY + 1);
        CheckNeighbours(isVisited, indiceX, indiceY - 1);
    }

    void ResetNeighbours(bool[,] isVisited, int indiceX, int indiceY)
    {
        if (indiceX < 0 || indiceX >= boardElementCount || indiceY < 0 || indiceY >= boardElementCount || isVisited[indiceX, indiceY] || !boardArray[indiceX, indiceY].clicked)
        {
            return;
        }
        boardArray[indiceX, indiceY].ResetElement();
        ResetNeighbours(isVisited, indiceX + 1, indiceY);
        ResetNeighbours(isVisited, indiceX - 1, indiceY);
        ResetNeighbours(isVisited, indiceX, indiceY + 1);
        ResetNeighbours(isVisited, indiceX, indiceY - 1);
    }


}
