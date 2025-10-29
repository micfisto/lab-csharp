namespace Lab_5.Interfaces;

public interface ITransoptCargo
{
    void LoadCargo(double loadWeightCargo);

    void IsPresenceOfRamp(bool presenceOfRamp);

    void UnloadCargo();
}