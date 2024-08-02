using NUnit.Framework;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace Tests.Runtime
{
    public class ObjectUtilitiesTests
    {
        private const string path = "Assets/TestAsset.asset";
        private const string invalidPath = "InvalidPath/TestAsset.asset";
        private TestScriptableObject _original;
        private TestScriptableObject _variant;

        [SetUp]
        public void SetUp()
        {
            _original = ScriptableObject.CreateInstance<TestScriptableObject>();
            _variant = ScriptableObject.CreateInstance<TestScriptableObject>();
            AssetDatabase.CreateAsset(_original, "Assets/original.asset");
        }
        
        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset("Assets/original.asset");
            AssetDatabase.DeleteAsset("Assets/original-variant.asset");
            AssetDatabase.DeleteAsset("Assets/original 1.asset");
            AssetDatabase.DeleteAsset(path);
        }
        
        [Test]
        public void SaveAsset_SavesNewAsset()
        {
            var asset = ScriptableObject.CreateInstance<TestScriptableObject>();
            asset.SaveAsset(path);
            Assert.IsTrue(AssetDatabase.Contains(asset));
        }
        
        [Test]
        public void SaveAsset_OverwritesExistingAsset()
        {
            var asset = ScriptableObject.CreateInstance<TestScriptableObject>();
            AssetDatabase.CreateAsset(asset, path);
            var newAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            newAsset.SaveAsset(path, overwrite: true);
            var savedAsset = AssetDatabase.LoadAssetAtPath<TestScriptableObject>(path);
            Assert.AreNotEqual(asset, savedAsset);
        }

        [Test]
        public void SaveAsset_DoesNotOverwriteExistingAsset()
        {
            var asset = ScriptableObject.CreateInstance<TestScriptableObject>();
            AssetDatabase.CreateAsset(asset, path);
            var newAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            newAsset.SaveAsset(path, overwrite: false);
            var savedAsset = AssetDatabase.LoadAssetAtPath<TestScriptableObject>(path);
            Assert.AreEqual(asset, savedAsset);
        }

        [Test]
        public void SaveAsset_InvalidPath_DoesNotSave()
        {
            var asset = ScriptableObject.CreateInstance<TestScriptableObject>();
            asset.SaveAsset(invalidPath);
            Assert.IsFalse(AssetDatabase.Contains(asset));
        }
        
        [Test]
        public void SaveVariant_CreatesNewAssetWithSuffix()
        {
            _original.SaveVariant(_variant, "variant");
            Assert.IsTrue(AssetDatabase.Contains(_variant));
            Assert.AreEqual("Assets/original-variant.asset", AssetDatabase.GetAssetPath(_variant));
        }

        [Test]
        public void SaveVariant_OriginalNotInAssetDatabase_DoesNothing()
        {
            var nonAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            nonAsset.SaveVariant(_variant, "variant");
            Assert.IsFalse(AssetDatabase.Contains(_variant));
        }

        [Test]
        public void DuplicateAsset_CreatesDuplicateAsset()
        {
            var duplicate = _original.DuplicateAsset();
            Assert.IsNotNull(duplicate);
            Assert.IsTrue(AssetDatabase.Contains(duplicate));
            Assert.AreEqual("Assets/original 1.asset", AssetDatabase.GetAssetPath(duplicate));
        }

        [Test]
        public void DuplicateAsset_OriginalNotInAssetDatabase_ReturnsNull()
        {
            var nonAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            var duplicate = nonAsset.DuplicateAsset();
            Assert.IsNull(duplicate);
        }

        [Test]
        public void DeleteAsset_DeletesExistingAsset()
        {
            _original.DeleteAsset();
            Assert.IsFalse(AssetDatabase.Contains(_original));
        }

        [Test]
        public void DeleteAsset_NonExistingAsset_DoesNothing()
        {
            var asset = ScriptableObject.CreateInstance<TestScriptableObject>();
            asset.DeleteAsset();
        }
    }
}