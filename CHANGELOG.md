# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2024-08-05

### Added
- `ObjectUtilities`: All methods from `ScriptableObjectUtilities` and a general save method for `Object` assets.
- `PrefabUtilities`: Utility methods for working with prefab assets.
- `package.json`: changelogUrl, licensesUrl, license and keywords fields.

### Changed
- `package.json`: Updated the package version to 1.1.0.

### Removed
- `ScriptableObjectUtilities`: All methods have been moved to `ObjectUtilities`.

### Fixed
- Scripts that use the `UnityEditor` namespace are now excluded from builds by being placed in an `Editor` folder.

## [1.0.0] - 2024-08-01

### Added
- `RefType<T>` and its children (e.g. `RefBool`): Classes for using built-in value types as reference types.
- `CameraUtilities`: Utility methods for checking visibility of points, bounds, and renderers from a camera.
- `ScriptableObjectUtilities`: Utility methods for saving, duplicating, and deleting `ScriptableObject` assets.
- `LayerMaskUtilities`: Utility methods for working with `LayerMask`.
