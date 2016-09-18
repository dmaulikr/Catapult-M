using UnityEngine;
using System.Collections;

public class WorldBounds : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		IDestructable d = other.transform.GetComponent<IDestructable> ();
		if (d != null) {
			d.getDestroyed ();
		} else {
			Destroy (other.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		print ("collide with " + other.transform.name);
	}	
}

public interface IDestructable
{
	void getDestroyed();
}