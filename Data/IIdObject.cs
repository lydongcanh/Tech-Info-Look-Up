namespace TechInfoLookUp.Data
{
    /// <summary>
    /// Base interface for all object which has an identity "id" field.
    /// </summary>
    /// <typeparam name="T">Type of the id.</typeparam>
    public interface IIdObject<T>
    {
        /// <summary>
        /// Identity.
        /// </summary>
        T Id { get; set; }
    }
}
