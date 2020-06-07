@ECHO OFF
courgette64.exe -apply Assembly-CSharp.dll Assembly-CSharp-2020.6.b.1.patch  Assembly-CSharp-patched.dll
rename Assembly-CSharp.dll Assembly-CSharp.dll.original
rename Assembly-CSharp-patched.dll Assembly-CSharp.dll