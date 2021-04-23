using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> playerInventory = new List<string>() {};

	void Start()
	{
		playerInventory.Add("Testname");
	}
}
