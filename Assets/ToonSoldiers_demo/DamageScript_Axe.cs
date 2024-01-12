using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript_Axe : MonoBehaviour
{
    public int damageCount_forEnemy = 30;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy_Damage>().TakeDamage(damageCount_forEnemy);
        }
    }
}

