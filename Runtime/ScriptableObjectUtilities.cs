using UnityEditor;
using UnityEngine;

public static class ScriptableObjectUtilities
{
	/// <summary>
	/// Saves a variant of the original ScriptableObject with a specified suffix.
	/// </summary>
	/// <param name="original">The original ScriptableObject asset.</param>
	/// <param name="variant">The variant ScriptableObject to save.</param>
	/// <param name="suffix">The suffix to append to the new asset's name.</param>
	public static void SaveVariant(this ScriptableObject original, ScriptableObject variant, string suffix)
	{
		if (!AssetDatabase.Contains(original)) return;
		string path = AssetDatabase.GetAssetPath(original).Replace(".asset", $"-{suffix}.asset");
		AssetDatabase.CreateAsset(variant, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
	/// <summary>
	/// Duplicates the original ScriptableObject asset.
	/// </summary>
	/// <param name="original">The original ScriptableObject asset to duplicate.</param>
	/// <returns>The duplicated ScriptableObject, or null if the original is not an asset.</returns>
	public static ScriptableObject DuplicateAsset(this ScriptableObject original)
	{
		if (!AssetDatabase.Contains(original)) return null;
		string path = AssetDatabase.GetAssetPath(original);
		string newPath = AssetDatabase.GenerateUniqueAssetPath(path);
		var duplicate = Object.Instantiate(original);
		AssetDatabase.CreateAsset(duplicate, newPath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		return duplicate;
	}
	
	/// <summary>
	/// Deletes the specified ScriptableObject asset.
	/// </summary>
	/// <param name="asset">The ScriptableObject asset to delete.</param>
	public static void DeleteAsset(this ScriptableObject asset)
	{
		if (!AssetDatabase.Contains(asset)) return;
		AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
}