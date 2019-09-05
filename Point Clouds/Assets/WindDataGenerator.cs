using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableFramework;
using ScriptableFramework.PointClouds;

public class WindDataGenerator : MonoBehaviour
{
	public Vector3List positions;

    // Start is called before the first frame update
    void Awake ()
    {
		InitialiseWind ();
    }

	private void InitialiseWind ()
	{
		for (float x = 0; x < 1000; x++)
		{
			for (float z = 0; z < 1000; z++)
			{
				positions.Add (new Vector3 (x, 0, z) / 100);
			}
		}
	}
}
