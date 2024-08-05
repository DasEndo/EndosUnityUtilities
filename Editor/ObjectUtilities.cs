using UnityEditor;
using UnityEngine;

public static class ObjectUtilities
{
	/// <summary>
	/// Saves an Object asset to the specified path.
	/// </summary>
	/// <param name="asset">The Object asset to save.</param>
	/// <param name="path">The path where the asset should be saved.</param>
	/// <param name="overwrite">Whether to overwrite if the asset already exists.</param>
	public static void SaveAsset(this Object asset, string path, bool overwrite = false)
	{
		if (!AssetDatabase.IsValidFolder(path[..path.LastIndexOf('/')])) return;
		if (AssetDatabase.LoadAssetAtPath<Object>(path) != null)
		{
			if (overwrite)
				AssetDatabase.DeleteAsset(path);
			else
				return;
		}
		AssetDatabase.CreateAsset(asset, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
	/// <summary>
	/// Saves a variant of the original Object with a specified suffix.
	/// </summary>
	/// <param name="original">The original Object asset.</param>
	/// <param name="variant">The variant Object to save.</param>
	/// <param name="suffix">The suffix to append to the new asset's name.</param>
	/// <param name="overwrite">Whether to overwrite if the variant already exists.</param>
	public static void SaveVariant(this Object original, Object variant, string suffix, bool overwrite = false)
	{
		if (!AssetDatabase.Contains(original)) return;
		string path = AssetDatabase.GetAssetPath(original).Replace(".asset", $"-{suffix}.asset");
		variant.SaveAsset(path, overwrite);
	}

	/// <summary>
	/// Duplicates the original Object asset.
	/// </summary>
	/// <param name="original">The original Object asset to duplicate.</param>
	/// <returns>The duplicated Object, or null if the original is not an asset.</returns>
	public static Object DuplicateAsset(this Object original)
	{
		if (!AssetDatabase.Contains(original)) return null;
		string path = AssetDatabase.GetAssetPath(original);
		string newPath = AssetDatabase.GenerateUniqueAssetPath(path);
		var duplicate = Object.Instantiate(original);
		duplicate.SaveAsset(newPath);
		return duplicate;
	}
	
	/// <summary>
	/// Deletes the specified Object asset.
	/// </summary>
	/// <param name="asset">The Object asset to delete.</param>
	public static void DeleteAsset(this Object asset)
	{
		if (!AssetDatabase.Contains(asset)) return;
		AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
}