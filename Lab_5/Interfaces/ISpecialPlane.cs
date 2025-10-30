namespace Lab_5.Interfaces;

public interface ISpecialPlane
{
    string CanLandOnUnpreparedSurface();
    string SpecialEquipment();
    void AddSpecialEquipment();
    void RemoveSpecialEquipment();
}