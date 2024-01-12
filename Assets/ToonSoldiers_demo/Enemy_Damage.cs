using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Damage : MonoBehaviour
{
    public  Animator animator;
    public int HP_Enemy;
    public  Image Bar_Enemy;
    public static bool gameOver_Enemy;
     void Start()
    {
        animator = GetComponent<Animator>();
        HP_Enemy = 100;
        gameOver_Enemy = false;
    }
    void Update()
    {
        Bar_Enemy.fillAmount = (float)HP_Enemy / 100;

        // if (gameOver_Enemy)
        // {
        //    animator.SetTrigger("Death_Enemy");
        //    GetComponent<Collider>().enabled=false;
        //    Bar_Enemy.gameObject.SetActive(false);
        // }
    }
    public void TakeDamage (int damageCount_forEnemy)
    {      
        HP_Enemy -= damageCount_forEnemy;
        if(HP_Enemy <= 0)
        {
           animator.SetTrigger("Death");
           GetComponent<Collider>().enabled=false;
           Bar_Enemy.gameObject.SetActive(false);
        }
    }
}
