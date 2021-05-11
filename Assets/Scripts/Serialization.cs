using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Classes;
using Classes.TryInHierarchie;
using Type = Classes.TryInHierarchie.EquipmentSlot.Type;

public static class Serialization
{
    [Serializable]
    public class SaveData
    {
        public List<ICanBePickedUp> InventoryContent { get; }
        public Dictionary<Type, EquipmentSlot> EquipmentSlots { get; }
        public WeaponsSlots WeaponsSlots { get; }

        public SaveData(List<ICanBePickedUp> inventoryContent, Dictionary<Type, EquipmentSlot> equipmentSlots,
            WeaponsSlots weaponsSlots)
        {
            InventoryContent = inventoryContent;
            EquipmentSlots = equipmentSlots;
            WeaponsSlots = weaponsSlots;
        }
    }

    private const string fileName = "veryImportantFile.bldk";

    public static void Save()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + $"/{fileName}.dat");

        var player = Player.Instance;
        var saveData = new SaveData(player.Inventory.Content, player.Robot.equipmentSlots, player.Robot.weaponsSlots);

        bf.Serialize(file, saveData);
        file.Close();
    }

    private static SaveData Load()
    {
        var bf = new BinaryFormatter();
        var file = File.Open(Application.persistentDataPath + $"/{fileName}.dat", FileMode.Open);
        
        var saveData = (SaveData) bf.Deserialize(file);
        file.Close();
        
        return saveData;
    }

    public static bool TryLoad(out SaveData saveData)
    {
        saveData = null;
        if (File.Exists(Application.persistentDataPath + $"/{fileName}.dat"))
        {
            saveData = Load();
            return true;
        }
        else
        {
            return false;
        }
    }
}