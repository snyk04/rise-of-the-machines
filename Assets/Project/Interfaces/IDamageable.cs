using Project.Classes.Characteristics;

namespace Project.Interfaces {
    public interface IDamageable {
        HealthCharacteristic Health { get; }
    }
}