using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField InputN;
    void Start()
    {
        InputN.text = Board.Instance.boardElementCount.ToString();
    }
    public void ChangeN(string newValue)
    {
        Board.Instance.boardElementCount = System.Convert.ToInt32(newValue);
    }
}
