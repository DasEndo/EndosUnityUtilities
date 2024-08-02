using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace Tests.Runtime
{
	public class PrefabUtilitiesTests
	{
		private GameObject _originalGameObject;
        private string _prefabPath;
        
        private readonly List<string> _prefabPaths = new();

        [SetUp]
        public void SetUp()
        {
            _originalGameObject = new GameObject("TestObject");
            _prefabPath = "Assets/TestObject.prefab";
            _prefabPaths.Add(_prefabPath);
            PrefabUtility.SaveAsPrefabAsset(_originalGameObject, _prefabPath);
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAssets(_prefabPaths.ToArray(), new List<string>());
            Object.DestroyImmediate(_originalGameObject);
            _prefabPaths.Clear();
        }

        [TestCase(10, 20, 30, "TestObject-x10°y20°z30°")]
        [TestCase(10, 0, 30, "TestObject-x10°z30°")]
        public void CreateRotatedPrefab_WithThreeRotations_CreatesNewPrefab_Using(float x, float y, float z, string expectedName)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(_prefabPath);
            var rotation = new Vector3(x, y, z);
            var result = prefab.CreateRotatedPrefab(rotation);
            _prefabPaths.Add(AssetDatabase.GetAssetPath(result));
            Assert.AreNotSame(prefab, result);
            Assert.That(x, Is.EqualTo(result.transform.rotation.eulerAngles.x).Within(0.01));
            Assert.That(y, Is.EqualTo(result.transform.rotation.eulerAngles.y).Within(0.01));
            Assert.That(z, Is.EqualTo(result.transform.rotation.eulerAngles.z).Within(0.01));
            Assert.AreEqual(expectedName, result.name);
        }

        [Test]
        public void CreateRotatedPrefab_WithZeroRotation_ReturnsSameGameObject()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(_prefabPath);
            var result = prefab.CreateRotatedPrefab(Vector3.zero);
            Assert.AreSame(prefab, result);
        }

        [Test]
        public void CreateRotatedPrefab_WithSingleAxisRotation_CreatesNewPrefab()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(_prefabPath);
            var result = prefab.CreateRotatedPrefab(45, Axis.X);
            _prefabPaths.Add(AssetDatabase.GetAssetPath(result));
            Assert.AreNotSame(prefab, result);
            Assert.That(45, Is.EqualTo(result.transform.rotation.eulerAngles.x).Within(0.01));
            Assert.AreEqual("TestObject-x45°", result.name);
        }

        [Test]
        public void CreateRotatedPrefab_WithZeroAngle_ReturnsSameGameObject()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(_prefabPath);
            var result = prefab.CreateRotatedPrefab(0, Axis.Y);
            Assert.AreSame(prefab, result);
        }
	}
}