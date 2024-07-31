using NUnit.Framework;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace Tests.Runtime
{
	public class LayerMaskUtilitiesTests
	{
		[Test]
		public void Contains_ReturnsTrue_WhenLayerIsInLayerMask()
		{
			LayerMask layerMask = 1 << 3;
			Assert.IsTrue(layerMask.Contains(3));
		}

		[Test]
		public void Contains_ReturnsFalse_WhenLayerIsNotInLayerMask()
		{
			LayerMask layerMask = 1 << 3;
			Assert.IsFalse(layerMask.Contains(2));
		}

		[Test]
		public void Contains_ReturnsTrue_WhenGameObjectLayerIsInLayerMask()
		{
			LayerMask layerMask = 1 << 3;
			GameObject gameObject = new GameObject { layer = 3 };
			Assert.IsTrue(layerMask.Contains(gameObject));
		}

		[Test]
		public void Contains_ReturnsFalse_WhenGameObjectLayerIsNotInLayerMask()
		{
			LayerMask layerMask = 1 << 3;
			GameObject gameObject = new GameObject { layer = 2 };
			Assert.IsFalse(layerMask.Contains(gameObject));
		}

		[Test]
		public void GetLayers_ReturnsAllLayersInLayerMask()
		{
			LayerMask layerMask = (1 << 3) | (1 << 5);
			int[] layers = layerMask.GetLayers();
			Assert.AreEqual(new[] { 3, 5 }, layers);
		}

		[Test]
		public void Fill_SetsLayerMaskWithSpecifiedLayers()
		{
			LayerMask layerMask = 0;
			layerMask.Fill(3, 5);
			Assert.IsTrue(layerMask.Contains(3));
			Assert.IsTrue(layerMask.Contains(5));
		}

		[Test]
		public void AddLayer_AddsLayerToLayerMask()
		{
			LayerMask layerMask = 1 << 3;
			layerMask.AddLayer(5);
			Assert.IsTrue(layerMask.Contains(3));
			Assert.IsTrue(layerMask.Contains(5));
		}

		[Test]
		public void RemoveLayer_RemovesLayerFromLayerMask()
		{
			LayerMask layerMask = (1 << 3) | (1 << 5);
			layerMask.RemoveLayer(3);
			Assert.IsFalse(layerMask.Contains(3));
			Assert.IsTrue(layerMask.Contains(5));
		}
	}
}