using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
	public Image cooldown;
	public float cooldownTime = 15f;
	
	// TODO: Connect to the actual cooldown
	// Refills the icon
	void Update ()
    {
		if (cooldown.fillAmount < 1)
			cooldown.fillAmount += 1.0f/cooldownTime * Time.deltaTime;	
	}
}
