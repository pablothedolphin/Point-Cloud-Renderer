using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableFramework.PointClouds
{
	[System.Serializable]
	public class Vector3Sampler : DataSampler<Vector3>
	{
		public Vector3List data;

		public override void SampleData ()
		{
			if (data != null && dataMap != null && baker != null)
			{
				dataList = data;
				BakeData (3); // passing in 3 to bake according to all 3 properties (x, y, z).
			}
		}
	}
}