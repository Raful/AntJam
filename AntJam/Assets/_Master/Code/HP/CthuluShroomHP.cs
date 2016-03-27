using UnityEngine;
using System.Collections;

public class CthuluShroomHP : HPComponent 
{
	protected override void Update ()
	{
		base.Update ();

		if (m_hp.currentHp <= 0)
		{
			GameLost ();
		}
	}

	private void GameLost()
	{
		Debug.Log ("Game lost");
		Time.timeScale = 0;
	}
}
