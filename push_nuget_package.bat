@ECHO OFF

echo Testing TaskTriangle
dotnet test Tests/TaskTriangleTests/TaskTriangleTests.csproj --no-build
if %errorlevel% neq 0 exit /b %errorlevel%

echo Pack TaskTriangle

echo Pushing package to github
set /p packageVersion="Enter package version: "
echo Pushing bin/debug/TaskTriangle.%packageVersion%.nupkg
dotnet nuget push TaskTriangle\bin\Debug\TaskTriangle.%packageVersion%.nupkg --source "github"