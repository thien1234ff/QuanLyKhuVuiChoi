@echo off
chcp 65001 > nul
title Phần Mềm Quản Lý Khu Vui Chơi Giải Trí - .NET 8 WinForms
echo ===================================================================
echo     HỆ THỐNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ (C# .NET 8 WINFORMS)
echo ===================================================================
echo.
echo Đang kiểm tra và khởi động ứng dụng...
echo.

set "DOTNET_ROOT=%USERPROFILE%\.dotnet"
set "PATH=%USERPROFILE%\.dotnet;%PATH%"

if exist "%~dp0bin\Debug\net8.0-windows\QuanLyKhuVuiChoi.exe" (
    echo Khởi chạy file exe thành công!
    start "" "%~dp0bin\Debug\net8.0-windows\QuanLyKhuVuiChoi.exe"
    exit /b 0
)

echo Đang build và chạy qua .NET 8 SDK...
"%USERPROFILE%\.dotnet\dotnet.exe" run

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [LỖI] Không thể chạy ứng dụng! Vui lòng kiểm tra lại.
    pause
)
