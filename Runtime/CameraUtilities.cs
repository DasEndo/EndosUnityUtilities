using System.Linq;
using UnityEngine;

public static class CameraUtilities
{ 
	/// <summary>
	/// Checks if a point is visible from a camera
	/// </summary>
	/// <param name="camera">The camera to check visibility from</param>
	/// <param name="point">The point to check visibility of</param>
	/// <returns>True if the point is visible from the camera, otherwise false</returns>
	public static bool IsPointVisible(this Camera camera, Vector3 point)
	{
		var screenPoint = camera.WorldToViewportPoint(point);
		return screenPoint.x is > 0 and < 1 && screenPoint.y is > 0 and < 1 && screenPoint.z > 0;
	}
	
	/// <summary>
	/// Checks if bounds are visible from a camera
	/// </summary>
	/// <param name="camera">The camera to check visibility from</param>
	/// <param name="bounds">The bounds to check visibility of</param>
	/// <returns>True if the bounds are visible from the camera, otherwise false</returns>
	public static bool IsBoundsVisible(this Camera camera, Bounds bounds)
	{
		var corners = new[]
		{
			bounds.min,
			new(bounds.min.x, bounds.min.y, bounds.max.z),
			new(bounds.min.x, bounds.max.y, bounds.min.z),
			new(bounds.min.x, bounds.max.y, bounds.max.z),
			new(bounds.max.x, bounds.min.y, bounds.min.z),
			new(bounds.max.x, bounds.min.y, bounds.max.z),
			new(bounds.max.x, bounds.max.y, bounds.min.z),
			bounds.max
		};
		return corners.Any(camera.IsPointVisible);
	}
	
	/// <summary>
	/// Checks if a skinned mesh renderer is visible from a camera
	/// </summary>
	/// <param name="camera">The camera to check visibility from</param>
	/// <param name="skinnedMeshRenderer">The skinned mesh renderer to check visibility of</param>
	/// <returns>True if the skinned mesh renderer is visible from the camera, otherwise false</returns>
	public static bool IsSkinnedMeshRendererVisible(this Camera camera, SkinnedMeshRenderer skinnedMeshRenderer)
	{
		return camera.IsBoundsVisible(skinnedMeshRenderer.bounds);
	}
	
	/// <summary>
	/// Checks if a mesh renderer is visible from a camera
	/// </summary>
	/// <param name="camera">The camera to check visibility from</param>
	/// <param name="meshRenderer">The mesh renderer to check visibility of</param>
	/// <returns>True if the mesh renderer is visible from the camera, otherwise false</returns>
	public static bool IsMeshRendererVisible(this Camera camera, MeshRenderer meshRenderer)
	{
		return camera.IsBoundsVisible(meshRenderer.bounds);
	}
	
	/// <summary>
	/// Checks if a collider is visible from a camera
	/// </summary>
	/// <param name="camera">The camera to check visibility from</param>
	/// <param name="collider">The collider to check visibility of</param>
	/// <returns>True if the collider is visible from the camera, otherwise false</returns>
	public static bool IsColliderVisible(this Camera camera, Collider collider)
	{
		return camera.IsBoundsVisible(collider.bounds);	
	}
}