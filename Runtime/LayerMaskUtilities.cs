using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LayerMaskUtilities
{
	/// <summary>
	/// Checks if the LayerMask contains a specific layer.
	/// </summary>
	/// <param name="layerMask">The LayerMask to check.</param>
	/// <param name="layer">The layer to check for.</param>
	/// <returns>True if the LayerMask contains the layer, otherwise false.</returns>
	public static bool Contains(this LayerMask layerMask, int layer)
	{
		return (layerMask | (1 << layer)) == layerMask;
	}
	
	/// <summary>
	/// Checks if the LayerMask contains the layer of a specific GameObject.
	/// </summary>
	/// <param name="layerMask">The LayerMask to check.</param>
	/// <param name="gameObject">The GameObject to check the layer of.</param>
	/// <returns>True if the LayerMask contains the GameObject's layer, otherwise false.</returns>
	public static bool Contains(this LayerMask layerMask, GameObject gameObject)
	{
		return layerMask.Contains(gameObject.layer);
	}
	
	/// <summary>
	/// Gets all layers contained in the LayerMask.
	/// </summary>
	/// <param name="layerMask">The LayerMask to get layers from.</param>
	/// <returns>An array of layers contained in the LayerMask.</returns>
	public static int[] GetLayers(this LayerMask layerMask)
	{
		var layers = new List<int>();
		for (int i = 0; i < 32; i++)
			if ((layerMask & (1 << i)) != 0)
				layers.Add(i);
		return layers.ToArray();
	}
	
	/// <summary>
	/// Fills the LayerMask with the specified layers.
	/// </summary>
	/// <param name="layerMask">The LayerMask to fill.</param>
	/// <param name="layers">The layers to fill the LayerMask with.</param>
	public static void Fill(this ref LayerMask layerMask, params int[] layers)
	{
		layerMask = layers.Aggregate<int, LayerMask>(0, (current, layer) => current | 1 << layer);
	}
	
	/// <summary>
	/// Adds a layer to the LayerMask.
	/// </summary>
	/// <param name="layerMask">The LayerMask to add the layer to.</param>
	/// <param name="layer">The layer to add.</param>
	public static void AddLayer(this ref LayerMask layerMask, int layer)
	{
		layerMask |= 1 << layer;
	}
	
	/// <summary>
	/// Removes a layer from the LayerMask.
	/// </summary>
	/// <param name="layerMask">The LayerMask to remove the layer from.</param>
	/// <param name="layer">The layer to remove.</param>
	public static void RemoveLayer(this ref LayerMask layerMask, int layer)
	{
		layerMask &= ~(1 << layer);
	}
}