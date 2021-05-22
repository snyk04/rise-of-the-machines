using Project.Interfaces;

namespace Project.Classes.CanBePickedUp.Items {
    public abstract class Item : ICanBePickedUp {

        public abstract bool TryPick();
    }
}