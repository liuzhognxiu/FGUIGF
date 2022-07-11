using GameFramework.DataTable;

namespace MetaArea
{
    public class BuildItemData : EntityDataBase
    {
        public BuildItemData(int entityId, int typeId, string buildNtfIndex) : base(entityId, typeId)
        {
            buildNTFIndex = buildNtfIndex;
        }

        private string buildNTFIndex;

        public string BuildNTFIndex
        {
            get => buildNTFIndex;
            set => buildNTFIndex = value;
        }

        public string Name
        {
            get
            {
                if (name=="")
                {
                    IDataTable<DREntity> drEntities = GameEntry.DataTable.GetDataTable<DREntity>();
                    DREntity drEntity = drEntities.GetDataRow(TypeId);
                    name = drEntity.AssetName;
                }
                return name;
            }
        }

        private string name;
    }
}