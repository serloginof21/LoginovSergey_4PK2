#include "framework.h"
#include "PZ_15.h"
#include <windows.h>

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
    LPSTR lpCmdLine, int nCmdShow) {
    HINSTANCE hInst;
    LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
    WNDCLASSEX wcex;
    HWND hWnd;
    MSG msg;

    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = LoadIcon(NULL, IDI_APPLICATION);
    wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszMenuName = NULL;
    wcex.lpszClassName = L"Win32App";
    wcex.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

    if (!RegisterClassEx(&wcex)) {
        MessageBox(NULL, L"Ошибка регистрации окна!", L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    hWnd = CreateWindowEx(0, L"Win32App", L"ПЗ 15",
        WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 500, 400, NULL, NULL, hInstance, NULL);

    if (hWnd == NULL) {
        MessageBox(NULL, L"Ошибка создания окна!", L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);

    while (GetMessage(&msg, NULL, 0, 0) > 0) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
    switch (message) {
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    case WM_MOVE:
        MessageBox(hWnd, L"Окно переместилось", L"Оповещение", MB_OK | MB_ICONINFORMATION);
        // Изменяем цвет заливки окна при получении сообщения WM_MOVE
        SetClassLongPtr(hWnd, GCLP_HBRBACKGROUND, (LONG_PTR)CreateSolidBrush(RGB(38, 145, 22)));
        RedrawWindow(hWnd, NULL, NULL, RDW_ERASE | RDW_INVALIDATE);
        break;
    case WM_ACTIVATE:
        // Изменяем цвет заливки окна при получении сообщения WM_ACTIVATE
        SetClassLongPtr(hWnd, GCLP_HBRBACKGROUND, (LONG_PTR)CreateSolidBrush(RGB(255, 0, 0)));
        RedrawWindow(hWnd, NULL, NULL, RDW_ERASE | RDW_INVALIDATE);
        break;
    case WM_RBUTTONDBLCLK:
        // Изменяем цвет заливки окна при получении сообщения WM_RBUTTONDBLCLK
        MessageBox(hWnd, L"Зачем кликаем?", L"Оповещение", MB_OK | MB_ICONINFORMATION);
        SetClassLongPtr(hWnd, GCLP_HBRBACKGROUND, (LONG_PTR)CreateSolidBrush(RGB(255, 0, 77)));
        RedrawWindow(hWnd, NULL, NULL, RDW_ERASE | RDW_INVALIDATE);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
        break;
    }
    return 0;
}