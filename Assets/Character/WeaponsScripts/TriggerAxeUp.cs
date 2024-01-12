using UnityEngine;
using System.Collections;

public class TriggerAxeUp : MonoBehaviour 
{
	public GameObject Rifle;
	public GameObject Rifle_Player;
    public GameObject Sword;
	public GameObject Sword_Player;
    public GameObject Axe;
	public GameObject Axe_Player;
	void Start()
	{	
		Axe.SetActive (false);
		Axe_Player.SetActive (false);
		Invoke("ShowAxe", 1f);
	}
	void ShowAxe()
	{
		Axe.SetActive(true);
	}
	void Update () 
	{
		// if (Input.GetKeyDown(KeyCode.E))
		// {
		// 	OnEKeyPressed();
		// }
		
	}
	void OnTriggerEnter(Collider col)
	{	
		
		if (col.tag == "Player" && !Axe.activeSelf)
		{
			return;
		}
		else if (col.tag == "Player" && Axe.activeSelf)
   		{
			Sword_Player.SetActive(false);
			Rifle_Player.SetActive(false);
			Axe_Player.SetActive(true);
			Axe_Player.GetComponent<Collider>().isTrigger = true;
			Sword_Player.GetComponent<Collider>().isTrigger = false;
			Axe.SetActive(false);
			Invoke("ShowAxe", 7f);
   		}
	}
}
