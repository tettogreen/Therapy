using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectPool : MonoBehaviour {

	public GameObject pooledObject;
	public int poolSize = 20;
	public bool willGrow = true;

	List<GameObject> pooledObjects;


	// Use this for initialization
	protected virtual void Awake ()
	{
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < poolSize; i++) {
			AddToPool();
		}
	}
	
	// Update is called once per frame
	public GameObject PoolObject ()
	{
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
//				pooledObjects [i].GetComponent<Destructible>().Init();
				return pooledObjects [i];
			}
		}

		if (willGrow) {
			return AddToPool();
		}

		return null;
	}

	GameObject AddToPool ()
	{
		GameObject obj = (GameObject)Instantiate(pooledObject, transform);
//		obj.transform.parent = gameObject.transform;
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}
}
