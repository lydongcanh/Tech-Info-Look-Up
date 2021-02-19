using System;

namespace TechInfoLookUp.Data.Entities
{
    /// <summary>
    /// <seealso cref="Entities.Tag"/> and <seealso cref="Entities.Tech"/> many to many relational class.
    /// </summary>
    public class TechTag : IIdObject<Tuple<int, int>>
    {
        /// <summary>
        /// <para>- Item1: <seealso cref="TechId"/></para>
        /// <para>- Item2: <seealso cref="TagId"/></para>
        /// </summary>
        public Tuple<int, int> Id
        {
            get => new Tuple<int, int>(TechId, TagId);
            set
            {
                TechId = value.Item1;
                TagId = value.Item2;
            }
        }

        public int TechId { get; set; }
        public Tech Tech { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
