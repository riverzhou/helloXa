using System;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;

namespace helloXa
{
    internal class BleScaner
    {
        readonly IBluetoothLE ble;
        readonly IAdapter adapter;

        readonly MainActivity main = null;

        public BleScaner(MainActivity mainActivity)
        {
            main = mainActivity;

            ble = CrossBluetoothLE.Current;
            adapter = ble.Adapter;
        }

        private void Print(String info)
        {
            main.Print(info);
        }

        public async Task StartAsync()
        {
            Print("开始扫描 ......");

            adapter.DeviceDiscovered += (s, a) =>
            {
                if (a.Device.Name != null)
                {
                    Print(a.Device.Name);
                }
            };

            await adapter.StartScanningForDevicesAsync();

            Print("扫描结束 ......");

        }
    }
}

