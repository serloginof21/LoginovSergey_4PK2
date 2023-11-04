#include <iostream>
#include <Windows.h>
#include <tchar.h>

void GetSystemInfo() {

    HKEY hKeyOS;
    std::wcout << "System" << std::endl;
    if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, TEXT("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"), 0, KEY_READ, &hKeyOS) == ERROR_SUCCESS) {
        TCHAR productName[256];
        DWORD productNameSize = sizeof(productName);
        if (RegQueryValueEx(hKeyOS, TEXT("ProductName"), NULL, NULL, (LPBYTE)productName, &productNameSize) == ERROR_SUCCESS) {
            std::wcout << L"Product Name: " << productName << std::endl;
        }
        TCHAR currentVersion[256];
        DWORD currentVersionSize = sizeof(currentVersion);
        if (RegQueryValueEx(hKeyOS, TEXT("CurrentVersion"), NULL, NULL, (LPBYTE)currentVersion, &currentVersionSize) == ERROR_SUCCESS) {
            std::wcout << L"Current Version: " << currentVersion << std::endl;
        }
        TCHAR buildLab[256];
        DWORD buildLabSize = sizeof(buildLab);
        if (RegQueryValueEx(hKeyOS, TEXT("BuildLab"), NULL, NULL, (LPBYTE)buildLab, &buildLabSize) == ERROR_SUCCESS) {
            std::wcout << L"Build Lab: " << buildLab << std::endl;
        }
        TCHAR systemRoot[256];
        DWORD systemRootSize = sizeof(systemRoot);
        if (RegQueryValueEx(hKeyOS, TEXT("SystemRoot"), NULL, NULL, (LPBYTE)systemRoot, &systemRootSize) == ERROR_SUCCESS) {
            std::wcout << L"System Root: " << systemRoot << std::endl;
        }
        RegCloseKey(hKeyOS);
    }
}

void GetBIOSInfo() {
    std::wcout << "BIOS" << std::endl;
    HKEY hKeyBIOS;
    if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, TEXT("HARDWARE\\DESCRIPTION\\System\\BIOS"), 0, KEY_READ, &hKeyBIOS) == ERROR_SUCCESS) {
        TCHAR biosVendor[256];
        DWORD biosVendorSize = sizeof(biosVendor);
        if (RegQueryValueEx(hKeyBIOS, TEXT("BIOSVendor"), NULL, NULL, (LPBYTE)biosVendor, &biosVendorSize) == ERROR_SUCCESS) {
            std::wcout << L"BIOS Vendor: " << biosVendor << std::endl;
        }

        TCHAR biosVersion[256];
        DWORD biosVersionSize = sizeof(biosVersion);
        if (RegQueryValueEx(hKeyBIOS, TEXT("BIOSVersion"), NULL, NULL, (LPBYTE)biosVersion, &biosVersionSize) == ERROR_SUCCESS) {
            std::wcout << L"BIOS Version: " << biosVersion << std::endl;
        }

        HKEY hKeySystem;
        if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, TEXT("HARDWARE\\DESCRIPTION\\System"), 0, KEY_READ, &hKeySystem) == ERROR_SUCCESS) {
            TCHAR systemManufacturer[256];
            DWORD systemManufacturerSize = sizeof(systemManufacturer);
            if (RegQueryValueEx(hKeySystem, TEXT("SystemManufacturer"), NULL, NULL, (LPBYTE)systemManufacturer, &systemManufacturerSize) == ERROR_SUCCESS) {
                std::wcout << L"System Manufacturer: " << systemManufacturer << std::endl;
            }

            RegCloseKey(hKeySystem);
        }
        RegCloseKey(hKeyBIOS);
    }
}

void GetStartupApplications() {
    std::wcout << "StartupApp" << std::endl;
    HKEY hKeyRun;
    if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, TEXT("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"), 0, KEY_READ, &hKeyRun) == ERROR_SUCCESS) {
        DWORD index = 0;
        TCHAR valueName[256];
        DWORD valueNameSize = sizeof(valueName);
        while (RegEnumValue(hKeyRun, index, valueName, &valueNameSize, NULL, NULL, NULL, NULL) == ERROR_SUCCESS) {
            std::wcout << L"Startup Application: " << valueName << std::endl;
            index++;
            valueNameSize = sizeof(valueName);
        }
        RegCloseKey(hKeyRun);
    }
}

int main() {
    GetSystemInfo();
    std::wcout << "\n" << std::endl;
    GetBIOSInfo();
    std::wcout << "\n" << std::endl;
    GetStartupApplications();
    return 0;
}