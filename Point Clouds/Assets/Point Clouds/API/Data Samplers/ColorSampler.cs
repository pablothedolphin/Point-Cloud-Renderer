using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableFramework.PointClouds
{
	[System.Serializable]
	public class ColorSampler : DataSampler<Color>
	{
		public ColorList data;

		public override void SampleData ()
		{
			if (data != null && dataMap != null && baker != null)
			{
				dataList = data;
				BakeData (4); // passing in 4 to bake according to all 4 properties (r, g, b, a).
			}
		}
	}
}