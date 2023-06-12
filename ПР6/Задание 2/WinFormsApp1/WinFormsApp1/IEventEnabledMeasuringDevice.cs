namespace MeasuringDevice
{
    interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        // Интервал Heartbeat, доступный только для чтения. Устанавливается только в конструкторе.
        int HeartBeatInterval { get; }

        delegate void HeartBeatEventHandler();
        // Делегат для события HeartBeat

        event HeartBeatEventHandler HeartBeat;
    }
}
