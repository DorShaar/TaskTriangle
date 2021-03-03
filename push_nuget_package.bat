@ECHO OFF

echo Testing TaskTriangle
dotnet test Tests/TaskTriangleTests/TaskTriangleTests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%

echo Packing TaskTriangle
set /p packageVersion="Enter package version: "
dotnet pack -p:PackageVersion=%packageVersion%

echo Pushing package to github
set /p packageVersion="Enter package version: "
echo Pusihg bin/debug/TaskTriangle.%packageVersion%.nupkg
dotnet nuget push TaskTriangle\bin\Debug\TaskTriangle.%packageVersion%.nupkg --source "github"