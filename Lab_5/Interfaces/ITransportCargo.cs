namespace Lab_5.Interfaces;

public interface ITransportCargo
{
    void LoadCargo(double loadWeightCargo);

    void IsPresenceOfRamp(bool presenceOfRamp);

    void UnloadCargo();
}