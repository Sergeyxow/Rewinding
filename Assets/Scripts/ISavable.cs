namespace DefaultNamespace
{
    public interface ISavable
    {
        ISavableData Save();

        void Load(ISavableData data);
    }
}