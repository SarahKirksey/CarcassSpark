@ECHO OFF

if exist Assembly-CSharp.dll.original (
	Echo Original file detected, reverting.
	del Assembly-CSharp.dll
	rename Assembly-CSharp.dll.original Assembly-CSharp.dll
)

@ECHO ON
courgette64.exe -apply Assembly-CSharp.dll Assembly-CSharp-2020.6.b.1-v5.patch  Assembly-CSharp-patched.dll
@ECHO OFF

if %errorlevel% EQU 0 ( goto SUCCESS ) else ( goto FAILURE )

:SUCCESS
Echo Patch Successful
rename Assembly-CSharp.dll Assembly-CSharp.dll.original
rename Assembly-CSharp-patched.dll Assembly-CSharp.dll
pause
exit /B

:FAILURE
Echo Patch Failed
pause