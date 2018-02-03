using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class control : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
