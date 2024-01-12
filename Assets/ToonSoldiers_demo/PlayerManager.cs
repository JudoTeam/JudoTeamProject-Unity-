using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int HP;
    public Image Bar;
    public static bool isBlocking;
    public static bool gameOver;
    //public TextMeshProUGUI playerHealthText;
    void Start()
    {
        HP = 500;
        gameOver = false;
        isBlocking = false;
    }

    void Update()
    {   
        Bar.fillAmount = (float)HP / 500;

        if (gameOver)
        {
            SceneManager.LoadScene("Level");
        }
    }

    public static void Damage (int damageCount)
    {
        if (!isBlocking)
       {
           HP -= damageCount;
       }

        if(HP <= 0)
        {
            gameOver = true;
        }
    }
}
