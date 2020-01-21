using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoardElement : MonoBehaviour
{
    int indiceX, indiceY;
    Image buttonImage;
    public bool clicked = false;
    
    [SerializeField]
    Sprite clickedSprite, nonClickedSprite;


    void Start()
    {
        buttonImage = GetComponent<Image>();
    }


    public void OnElementClick()
    {
        if (!clicked)
        {
            buttonImage.sprite = clickedSprite;
            clicked = true;
            Board.Instance.CheckAddedElement(indiceX, indiceY);
        }

    }

    public void SetIndices(int x, int y)
    {
        indiceX = x;
        indiceY = y;
    }

    public void ResetElement(){
        clicked = false;
        buttonImage.sprite = nonClickedSprite;
    }



}
