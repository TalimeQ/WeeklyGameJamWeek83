using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreBoardText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int newScore)
    {
        scoreBoardText.SetText(": " + newScore);
    }
}
