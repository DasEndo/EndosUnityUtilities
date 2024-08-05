using UnityEditor;
using UnityEngine;

public static class PrefabUtilities
{
	public static GameObject CreateRotatedPrefab(this GameObject original, Vector3 rotationAngles)
	{
		if (rotationAngles == Vector3.zero || !AssetDatabase.Contains(original)) return original;
		string rotationText = $"{(rotationAngles.x != 0 ? $"x{rotationAngles.x}°" : "")}" +
							  $"{(rotationAngles.y != 0 ? $"y{rotationAngles.y}°" : "")}" +
							  $"{(rotationAngles.z != 0 ? $"z{rotationAngles.z}°" : "")}";
		string prefabPath = AssetDatabase.GetAssetPath(original).Replace(".prefab", $"-{rotationText}.prefab");
		var newPrefab = PrefabUtility.SaveAsPrefabAsset(original, prefabPath);
		newPrefab.transform.Rotate(rotationAngles);
		return newPrefab;
	}
	
	public static GameObject CreateRotatedPrefab(this GameObject original, float angle, Axis axis)
	{
		if (angle == 0) return original;
		var rotationAngles = Vector3.zero;
		switch (axis)
		{
			case Axis.X:
				rotationAngles.x = angle;
				break;
			case Axis.Y:
				rotationAngles.y = angle;
				break;
			case Axis.Z:
				rotationAngles.z = angle;
				break;
		}
		return CreateRotatedPrefab(original, rotationAngles);
	}
}