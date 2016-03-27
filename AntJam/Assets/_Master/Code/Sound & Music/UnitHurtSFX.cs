using UnityEngine;
using System.Collections;

/// <summary>
/// Class plays the active sound event when a unit is hurt.
/// Requires active sound event to be ally/enemy hurt sound event.
/// </summary>
public class UnitHurtSFX : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        var hpc = gameObject.GetComponent<HPComponent>();
        if (hpc.isHurt && hpc.isAlive)
        {
            gameObject.GetComponent<EventPlayer>().PlayEvent();
            hpc.isHurt = false;
        }
    }

    void OnDestroy()
    {
        gameObject.GetComponent<EventPlayer>().ChangeParameter("isDead", 1f);
        gameObject.GetComponent<EventPlayer>().PlayEvent();
    }
}
