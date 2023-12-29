using UnityEngine;
using System.Collections;

public class TriggerAxeUp : MonoBehaviour 
{
	public GameObject Axe;
	void Update () 
	{
	
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
		{
			Axe.SetActive (true);
			Destroy (gameObject);
		}
	}
}
