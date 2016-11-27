using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class ActivateTriggerOnDisable : ActivateTrigger  {

	void OnDisable ()
	{
		DoActivateTrigger();
	}
}
