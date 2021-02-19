using System;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.API.Tag
{
    public class TagPostRequest
    {
        public string Name { get; set; }

        /// <summary>
        /// Long (HTML) detailed infomation.
        /// </summary>
        public string Info { get; set; }

        public Entities.Tag ToTag()
        {
            return new Entities.Tag()
            {
                Name = Name,
                Info = Info
            };
        }
    }
}
