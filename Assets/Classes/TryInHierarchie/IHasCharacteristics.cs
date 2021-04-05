using System.Collections.Generic;

namespace Classes.TryInHierarchie {
    public interface IHasCharacteristics {
        Dictionary<Characteristic.Type, Characteristic> Stats { get; }
    }
}