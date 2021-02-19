using System.Collections.Generic;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.API.Tech
{
    public class TechPostRequest
    {
        public string Name { get; set; }

        /// <summary>
        /// Short description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Long (HTML) detailed infomation.
        /// </summary>
        public string Info { get; set; }

        public List<int> TagIds { get; set; }

        public Entities.Tech ToTech(IEnumerable<TechTag> techTags = null)
        {
            return new Entities.Tech()
            {
                Name = Name,
                Description = Description,
                Info = Info,
                TechTags = techTags
            };
        }
    }
}
