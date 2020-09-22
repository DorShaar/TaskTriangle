using System.Collections.Generic;

namespace Triangle.Resources
{
    public class TaskResources
    {
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