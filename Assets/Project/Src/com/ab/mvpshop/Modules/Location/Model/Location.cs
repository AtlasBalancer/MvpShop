using System;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.modules.location
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