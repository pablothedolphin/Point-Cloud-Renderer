using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableFramework.PointClouds
{
	public interface IDataSampler
	{
		void SampleData ();
		void BakeData (int propertyCount);
	}
}