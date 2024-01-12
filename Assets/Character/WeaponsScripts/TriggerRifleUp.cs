using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRifleUp : MonoBehaviour
{
    public GameObject Rifle;
	public GameObject Rifle_Player;
    public GameObject Sword;
	public GameObject Sword_Player;
    public GameObject Axe;
	public GameObject Axe_Player;
    void Start()
	{	
		Rifle.SetActive (false);
		Rifle_Player.SetActive (false);
		Invoke("ShowRifle", 15f);
	}
	void ShowRifle()
	{
		Rifle.SetActive(true);
	}
    void OnTriggerEnter(Collider col)
	{	
	// 	if (!Axe.activeSelf)
	// 	{
	// 		return;
	// 	}
	// 	else if (col.tag == "Player") 
	// 	{
	// 		Axe_Player.SetActive (true);
	// 		Axe.SetActive (false);
	// 	}

		if (col.tag == "Player" && !Rifle.activeSelf)
		{
			return;
		}
		else if (col.tag == "Player" && Rifle.activeSelf)
   		{
			Sword_Player.SetActive(false);
            Axe_Player.SetActive(false);
			Rifle_Player.SetActive(true);
			Rifle.SetActive(false);
			Invoke("ShowRifle", 20f);
   		}
	}
}
