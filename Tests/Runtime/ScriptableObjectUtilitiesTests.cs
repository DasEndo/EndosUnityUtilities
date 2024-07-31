using NUnit.Framework;
using UnityEngine;
using UnityEditor;

// ReSharper disable InconsistentNaming
namespace Tests.Runtime
{
	public class TestScriptableObject : ScriptableObject { }
	
	public class ScriptableObjectUtilitiesTests
	{
        private TestScriptableObject _original;
        private TestScriptableObject _variant;

        [SetUp]
        public void SetUp()
        {
            _original = ScriptableObject.CreateInstance<TestScriptableObject>();
            _variant = ScriptableObject.CreateInstance<TestScriptableObject>();
            AssetDatabase.CreateAsset(_original, "Assets/original.asset");
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
        public void DeleteAsset_RemovesAssetFromDatabase()
        {
            _original.DeleteAsset();
            Assert.IsFalse(AssetDatabase.Contains(_original));
        }

        [Test]
        public void DeleteAsset_AssetNotInDatabase_DoesNothing()
        {
            var nonAsset = ScriptableObject.CreateInstance<TestScriptableObject>();
            nonAsset.DeleteAsset();
            Assert.IsFalse(AssetDatabase.Contains(nonAsset));
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset("Assets/original.asset");
            AssetDatabase.DeleteAsset("Assets/original-variant.asset");
            AssetDatabase.DeleteAsset("Assets/original 1.asset");
        }
    }
}