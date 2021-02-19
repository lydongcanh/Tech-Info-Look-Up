using System.Collections.Generic;

namespace TechInfoLookUp.Data.Entities
{
    /// <summary>
    /// Ex: Framework, programming languages,...
    /// </summary>
    /// TODO: Rename this to Type or something cleaner...
    public class Tag : IIdObject<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Long (HTML) detailed info.
        /// </summary>
        public string Info { get; set; }

        public IEnumerable<Tech> Techs { get; set; }
    }
}
