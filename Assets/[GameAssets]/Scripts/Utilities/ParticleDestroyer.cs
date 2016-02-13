using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {
	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}


	public void Update() 
	{
			if(!ps.IsAlive())
			{
				Destroy(gameObject);
			}
	}
}
