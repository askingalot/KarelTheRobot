namespace KarelTheRobot
{
    public abstract class WorldObject
    {
        public WorldObject(int street, int avenue)
        {
            Street = street;
            Avenue = avenue;
        }

        /// <Summary>
        /// Run East - West
        /// </Summary>
        public int Street { get; protected set; }

        /// <Summary>
        /// Run North - South
        /// </Summary>
        public int Avenue { get; protected set; }
    }
}