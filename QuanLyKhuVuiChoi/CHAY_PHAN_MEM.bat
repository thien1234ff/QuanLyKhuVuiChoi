@echo off
chcp 65001 > nul
title Phần Mềm Quản Lý Khu Vui Chơi Giải Trí - .NET 8 WinForms
echo ===================================================================
echo     HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ (C# .NET 8 WINFORMS)
echo ===================================================================
echo.
echo Đang biên dịch các thay đổi mới nhất (UI, Code)...
echo.

set "DOTNET_ROOT=%USERPROFILE%\.dotnet"
set "PATH=%USERPROFILE%\.dotnet;%PATH%"

dotnet build "%~dp0QuanLyKhuVuiChoi.csproj" -c Debug --nologo -v q
if %ERRORLEVEL% EQU 0 (
    echo [OK] Đã cập nhật xong thay đổi! Đang khởi động ứng dụng...
    start "" "%~dp0bin\Debug\net8.0-windows\QuanLyKhuVuiChoi.exe"
    exit /b 0
) else (
    echo.
    echo [LỖI] Có lỗi trong mã nguồn hoặc giao diện khi biên dịch!
    pause
)

