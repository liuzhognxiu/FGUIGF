namespace MetaArea
{
    public class MainPlayerData : EntityDataBase
    {
        public MainPlayerData(int entityId, int typeId) : base(entityId, typeId)
        {

        }

        public string Name;

        public HexCell CurCell;
    }
}