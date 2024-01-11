using UnityEngine;

namespace YNL.Utilities
{
    [CreateAssetMenu(fileName = "Sources Storage", menuName = "YのL/Sources Storage")]
    public class SourceStorage : ScriptableObject
    {
        public SerializableDictionary<string, Sprite> Sprites = new();
        public SerializableDictionary<string, Texture2D> Texture2s = new();
        public SerializableDictionary<string, GameObject> Objects = new();
        public SerializableDictionary<string, AudioClip> AudioClips = new();
        public SerializableDictionary<string, TextAsset> Jsons = new();
    }
}