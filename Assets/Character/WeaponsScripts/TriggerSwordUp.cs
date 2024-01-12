using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerSwordUp : MonoBehaviour
{
	public GameObject Rifle;
	public GameObject Rifle_Player;
    public GameObject Sword;
	public GameObject Sword_Player;
    public GameObject Axe;
	public GameObject Axe_Player;
	void Start()
	{	
		Sword.SetActive (false);
		Sword_Player.SetActive (false);
		Invoke("ShowSword", 1f);
	}
	void ShowSword()
	{
		Sword.SetActive(true);
	}
	void Update () 
	{
		
	}
	void OnTriggerEnter(Collider col)
	{	
		if (col.tag == "Player" && !Sword.activeSelf)
		{
			return;
		}
		else if (col.tag == "Player" && Sword.activeSelf) 
		{
			Axe_Player.SetActive(false);
			Rifle_Player.SetActive(false);
			Sword_Player.SetActive (true);
			Sword_Player.GetComponent<Collider>().isTrigger = true;
			Axe_Player.GetComponent<Collider>().isTrigger = false;
			Sword.SetActive (false);
			Invoke("ShowSword", 20f);
		}
	}
}
