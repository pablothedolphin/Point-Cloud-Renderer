using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace ScriptableFramework.PointClouds
{
	public enum SampleMode { Always, OnEnable, ManualOnly }

	[RequireComponent (typeof (VisualEffect))]
	public class PointCloudRenderer : MonoBehaviour
	{
		public SampleMode sampleMode;

		public ColorSampler colorSampler;
		public Vector3Sampler positionSampler;
		public Vector3Sampler velocitySampler;
		public Vector3Sampler normalSampler;

		private VisualEffect pointCloud;

		private void Awake ()
		{
			pointCloud = GetComponent<VisualEffect> ();
		}

		void OnEnable ()
		{
			if (sampleMode != SampleMode.ManualOnly) Resample ();
		}

		void Update ()
		{
			if (sampleMode == SampleMode.Always) Resample ();
		}

		public void Resample ()
		{
			if (colorSampler.shouldSample) colorSampler.SampleData ();
			if (positionSampler.shouldSample) positionSampler.SampleData ();
			if (velocitySampler.shouldSample) velocitySampler.SampleData ();
			if (normalSampler.shouldSample) normalSampler.SampleData ();
		}
	}
}