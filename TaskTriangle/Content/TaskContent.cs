using Newtonsoft.Json;
using System.Collections.Generic;

namespace Triangle.Content
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TaskContent
    {
        [JsonProperty]
        private readonly Dictionary<string, bool> mContents = new Dictionary<string, bool>();

        public IReadOnlyDictionary<string, bool> GetContents()
        {
            return mContents;
        }

        public bool AddContent(string content)
        {
            return mContents.TryAdd(content, false);
        }

        public bool MarkContentDone(string content)
        {
            if (!mContents.ContainsKey(content))
                return false;

            mContents[content] = true;
            return true;
        }
    }
}