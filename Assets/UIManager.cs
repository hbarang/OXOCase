using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField InputN;

    [SerializeField]
    TextMeshProUGUI matchCount;
    void Start()
    {
        InputN.text = Board.Instance.boardElementCount.ToString();
        matchCount.text = "0";
        Board.Instance.MatchCountChangedEvent += ChangeMatchCount;
    }

    void ChangeMatchCount(int matches)
    {
        matchCount.text = matches.ToString();
    }

    void OnDestroy()
    {
        Board.Instance.MatchCountChangedEvent -= ChangeMatchCount;
    }

    public void ChangeN(string newValue)
    {
        Board.Instance.boardElementCount = System.Convert.ToInt32(newValue);
    }



}
