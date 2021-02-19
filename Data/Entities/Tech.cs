using System.Collections.Generic;

namespace TechInfoLookUp.Data.Entities
{
    /// <summary>
    /// Ex: ASP.NET, C#, React, Javascript,...
    /// </summary>
    public class Tech : IIdObject<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Short description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Long (HTML) detailed infomation.
        /// </summary>
        public string Info { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
