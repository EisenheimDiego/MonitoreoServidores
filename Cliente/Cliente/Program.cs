using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            float cpuUsage = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            cpuUsage = cpuCounter.NextValue();

            // Uso de memoria
            var memCounter = new PerformanceCounter("Memory", "Available MBytes");
            float memUsage = memCounter.NextValue();

            // Uso de disco duro
            var drive = new DriveInfo("C");
            long totalSize = drive.TotalSize;
            long totalFreeSpace = drive.TotalFreeSpace;
            float diskUsage = ((float)(totalSize - totalFreeSpace)) / totalSize * 100;
        }
    }
}
