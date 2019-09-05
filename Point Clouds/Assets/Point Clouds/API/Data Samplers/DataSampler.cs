using System;
using UnityEngine;

namespace ScriptableFramework.PointClouds
{
	public abstract class DataSampler<T> : IDataSampler, IDisposable
	{
		public bool shouldSample;
		[SerializeField] protected RenderTexture dataMap;
		[SerializeField] protected ComputeShader baker;

		protected RuntimeList<T> dataList;

		protected ComputeBuffer dataBuffer;
		protected RenderTexture tempDataMap;

		public abstract void SampleData ();

		public void Dispose ()
		{
			if (dataBuffer != null) dataBuffer.Dispose ();
			dataBuffer = null;

			if (tempDataMap != null) GameObject.Destroy (tempDataMap);
			tempDataMap = null;
		}

		public void BakeData (int propertyCount)
		{
			if (!shouldSample) return;

			int mapWidth = dataMap.width;
			int mapHeight = dataMap.height;

			int totalData = dataList.Count;
			int totalProperties = totalData * propertyCount;

			// Release the temporary objects when the size of them don't match
			// the input.

			if (dataBuffer != null && dataBuffer.count != totalProperties)
			{
				dataBuffer.Dispose ();
				dataBuffer = null;
			}

			if (tempDataMap != null && (tempDataMap.width != mapWidth || tempDataMap.height != mapHeight))
			{
				GameObject.Destroy (tempDataMap);
				tempDataMap = null;
			}

			// Lazy initialization of temporary objects

			if (dataBuffer == null)
			{
				dataBuffer = new ComputeBuffer (totalProperties, sizeof (float));
			}

			if (tempDataMap == null)
			{
				tempDataMap = CreateRenderTexture (dataMap);
			}

			// Set data and execute the bake task.

			baker.SetInt ("DataCount", totalData);
			dataBuffer.SetData (dataList.ToArray ());
			baker.SetBuffer (0, "DataBuffer", dataBuffer);
			baker.SetTexture (0, "DataMap", tempDataMap);

			baker.Dispatch (0, mapWidth / 8, mapHeight / 8, 1);

			// once complete, write the results back on to the real data map file

			Graphics.CopyTexture (tempDataMap, dataMap);
		}

		private void CheckConsistency ()
		{
			if (dataMap.width % 8 != 0 || dataMap.height % 8 != 0)
			{
				Debug.LogError ("Data map dimensions should be a multiple of 8.");
			}

			if (dataMap.format != RenderTextureFormat.ARGBHalf && dataMap.format != RenderTextureFormat.ARGBFloat)
			{
				Debug.LogError ("Data map format should be ARGBHalf or ARGBFloat.");
			}
		}

		private RenderTexture CreateRenderTexture (RenderTexture source)
		{
			var rt = new RenderTexture (source.width, source.height, 0, source.format);
			rt.enableRandomWrite = true;
			rt.Create ();
			return rt;
		}
	}
}
