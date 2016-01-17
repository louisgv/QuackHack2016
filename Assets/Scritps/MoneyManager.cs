using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyManager : MonoBehaviour {

	public int resources = 240;
	[SerializeField] private Text remaining_money_display;

	public bool AttemptPurchase(int price)
	{
		if (resources < price)
			return false;
		resources -= price;
		return true;
	}

	void Update()
	{
		remaining_money_display.text = resources.ToString();
	}

}
