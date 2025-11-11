using System;
using com.ab.mvcshop.core.mvc;

namespace com.ab.mvcshop.modules.location
{
    [Serializable]
    public class Location : IModel
    {
        public Location() => 
            Title = default;

        public string Title;

        public Location(string title) => 
            Title = title;

        public void Combine(IModel model)
        {
            //TODO: Need to figure out how to combine locations.
        }
    }
}