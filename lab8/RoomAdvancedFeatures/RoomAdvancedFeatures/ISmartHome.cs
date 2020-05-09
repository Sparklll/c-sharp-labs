using System;

namespace RoomAdvancedFeatures
{
    public interface ISmartHome
    {
        void SwitchOnControlDevice();
        void ConfigureSystem();
        void AnalyzeSensors();
        void SetupSensors();
        public event EventHandler StartUp;
    }
}