namespace KarelTheRobot.Library
{
    public abstract class WorldObject
    {
        internal WorldObject(int street, int avenue)
        {
            Street = street;
            Avenue = avenue;
        }

        /// <Summary>
        /// Run East - West
        /// </Summary>
        internal int Street { get; set; }

        /// <Summary>
        /// Run North - South
        /// </Summary>
        internal int Avenue { get; set; }
    }
}