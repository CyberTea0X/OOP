namespace MeasuringDevice
{
    interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        event EventHandler NewMeasurementTaken;
        // Событие, которое должно срабатывать при каждом Heartbeat

        delegate void HeartBeatEventHandler();
        // Делегат для события HeartBeat

        event HeartBeatEventHandler HeartBeat;
        // Интервал Heartbeat, доступный только для чтения. Устанавливается только в конструкторе.
        int HeartBeatInterval { get; }
    }
}
