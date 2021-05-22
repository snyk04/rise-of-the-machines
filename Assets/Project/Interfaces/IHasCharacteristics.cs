using System.Collections.Generic;
using Project.Classes.Characteristics;

namespace Project.Interfaces {
    public interface IHasCharacteristics {
        Dictionary<Characteristic.Type, Characteristic> Stats { get; }
    }
}