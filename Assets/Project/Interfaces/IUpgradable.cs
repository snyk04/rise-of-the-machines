namespace Project.Interfaces {
    public interface IUpgradable {
        int Level { get; }
        void Upgrade();
    }
}