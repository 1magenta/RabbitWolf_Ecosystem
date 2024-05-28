using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerMonkey : MonoBehaviour
{
    public TextMeshProUGUI monkeyCountText;  // If using TextMeshPro, replace 'Text' with 'TextMeshProUGUI'.
    public TextMeshProUGUI foodCountText;

    void Update()
    {
        monkeyCountText.text = "Monkey: " + CountManagerMonkey.Instance.MonkeyCount;
        foodCountText.text = "Food: " + CountManagerMonkey.Instance.FoodCount;

    }
}
