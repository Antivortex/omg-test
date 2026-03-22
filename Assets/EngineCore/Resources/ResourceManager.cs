using UnityEngine;

namespace EngineCore.Resources
{
    public class ResourceManager : IResourceManager
    {
        private readonly string _basePath;

        public ResourceManager(string basePath)
        {
            _basePath = basePath;
        }

        public Sprite LoadSprite(string name)
        {
            var path = _basePath + name;
            var sprite = UnityEngine.Resources.Load<Sprite>(path);

            if (sprite == null)
                Debug.LogWarning($"Sprite not found at path: {path}");

            return sprite;
        }
    }
}
