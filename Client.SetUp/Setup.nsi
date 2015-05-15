!include "FileFunc.nsh"
!include "StrFunc.nsh"
!include LogicLib.nsh
!include x64.nsh

;The name of the installer
Name "InstallOptions Test"

!ifdef SETUP_DIR
	OutFile "${SETUP_DIR}\Setup.exe"
!else
	OutFile "Setup.exe"
!endif
!ifdef SOURCE_FILES
!else 
	!define SOURCE_FILES "..\..\..\Client"
!endif

ShowInstDetails show

Function .onInit
	${GetParameters} $R0
	${GetOptions} $R0 "/INSTDIR=" $INSTDIR
	${GetOptions} $R0 "/SILENT=" $0
	${If} $0 == 'TRUE'
		SetSilent silent
	${Else}
		SetSilent normal
	${EndIf}

	SetOutPath $INSTDIR 

FunctionEnd

Section
 
	SetOverwrite ifnewer 

	CreateDirectory $INSTDIR
	File /r /x 'app_test' /x 'bin' /x 'obj' /x 'Properties' /x 'Client.csproj' /x 'Client.csproj.user' /x 'packages.config' '${SOURCE_FILES}\*'
    WriteUninstaller $INSTDIR\uninstaller.exe

	DetailPrint "Creating ISS virtual directory"

	${If} ${RunningX64}
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" delete site webapp'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" add site /name:"webapp" /physicalPath:"$INSTDIR" /bindings:http://*:16128'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" add apppool /name:webappPool'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" set apppool webappPool /managedRuntimeVersion:v4.0'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" set apppool webappPool /enable32BitAppOnWin64:True'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" set app "webapp/" /applicationPool:webappPool'
	${Else}
	    ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" delete site webapp'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" add site /name:webapp /physicalPath:"$INSTDIR" /bindings:http://*:16128'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" add apppool /name:webappPool'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" set apppool webappPool /managedRuntimeVersion:v4.0'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" set apppool webappPool /enable32BitAppOnWin64:True'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" set app "webapp/" /applicationPool:webappPool'
	${EndIf}     
	

SectionEnd

Section "Uninstall"

	${If} ${RunningX64}
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" delete site webapp'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" delete apppool webappPool'
	${Else}
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" delete site webapp'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" delete apppool webappPool'
	${EndIf}  

	Delete $INSTDIR\*.*
	RMDir /r $INSTDIR
SectionEnd
