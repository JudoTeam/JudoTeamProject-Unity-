using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript_Sword : MonoBehaviour
{
    public int damageCount_forEnemy = 10;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy_Damage>().TakeDamage(damageCount_forEnemy);
        }
    }
}
