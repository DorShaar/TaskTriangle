using Newtonsoft.Json;
using System.Collections.Generic;

namespace Triangle.Resources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TaskResources
    {
        [JsonProperty]
        private readonly List<string> mResources = new List<string>();

        public IEnumerable<string> GetResources()
        {
            return mResources;
        }

        public void AddResource(string resource)
        {
            mResources.Add(resource);
        }
    }
}