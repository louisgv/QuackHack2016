using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
	public static int resources = 240;
	
	private Text remaining_money_display;

	public void Start ()
	{
		remaining_money_display = GetComponent<Text> ();
	}

	public bool AttemptPurchase (int price)
	{
		if (resources < price)
			return false;
		resources -= price;
		return true;
	}

	void Update ()
	{
		remaining_money_display.text = resources.ToString ();
	}

}
