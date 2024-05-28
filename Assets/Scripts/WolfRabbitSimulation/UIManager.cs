using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI plantCountText; 
    public TextMeshProUGUI rabbitCountText;
    public TextMeshProUGUI wolfCountText;

    void Update()
    {
        plantCountText.text = "Plants: " + CountManager.Instance.PlantCount;
        rabbitCountText.text = "Rabbits: " + CountManager.Instance.RabbitCount;
        wolfCountText.text = "Wolves: " + CountManager.Instance.WolfCount;
    }
}
