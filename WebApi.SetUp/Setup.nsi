!include "FileFunc.nsh"
!include "StrFunc.nsh"
!include LogicLib.nsh
!include x64.nsh

!macro AdjustConfigValue ConfigFile Key Value
 
   DetailPrint "Config: adding '${Key}'='${Value}' to ${ConfigFile}"
 
   nsisXML::create
   nsisXML::load ${ConfigFile}
 
   nsisXML::select "/configuration/appSettings/add[@key='${Key}']"
   nsisXML::setAttribute "value" ${Value}
 
   nsisXML::save ${ConfigFile}
 
!macroend

${StrRep}

;The name of the installer
Name "InstallOptions Test"

!ifdef SETUP_DIR
	OutFile "${SETUP_DIR}\Setup.exe"
!else
	OutFile "Setup.exe"
!endif
!ifdef SOURCE_FILES
!else 
	!define SOURCE_FILES "..\..\..\Scenario.WebAPI"
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

	${GetOptions} $R0 "/UPDATECONFIG=" $0

	SetOutPath $INSTDIR 

FunctionEnd

Section
 
	SetOverwrite ifnewer 

	CreateDirectory $INSTDIR
	File /r ${SOURCE_FILES}\*.*
    WriteUninstaller $INSTDIR\uninstaller.exe
	DetailPrint "updating config file? $0"
	;importante l'ordine per push / pull
	${If} $0 == 'TRUE'
		DetailPrint "updating config file"
		ip::get_ip
		Pop $R1
		${StrRep} $R1 $R1 ";" ""
		!insertmacro AdjustConfigValue "$INSTDIR\Web.config" "AccessControlAddress" "http://*:16128"
	${EndIf}
	DetailPrint "Creating ISS virtual directory"

	${If} ${RunningX64}
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" delete site webapi'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" add site /name:"webapi" /physicalPath:"$INSTDIR" /bindings:http://*:2071'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" add apppool /name:webapiPool'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" set apppool webapiPool /managedRuntimeVersion:v4.0'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" set apppool webapiPool /enable32BitAppOnWin64:True'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" set app "webapi/" /applicationPool:webapiPool'
	${Else}
	    ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" delete site webapi'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" add site /name:webapi /physicalPath:"$INSTDIR" /bindings:http://$R1:2071'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" add apppool /name:webapiPool'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" set apppool webapiPool /managedRuntimeVersion:v4.0'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" set apppool webapiPool /enable32BitAppOnWin64:True'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" set app "webapi/" /applicationPool:webapiPool'
	${EndIf}     
	

SectionEnd

Section "Uninstall"

	${If} ${RunningX64}
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" delete site webapi'
		ExecWait '"C:\Windows\SysWOW64\inetsrv\appcmd.exe" delete apppool webapiPool'
	${Else}
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" delete site webapi'
		ExecWait '"C:\Windows\System32\inetsrv\appcmd.exe" delete apppool webapiPool'
	${EndIf}  

	Delete $INSTDIR\*.*
	RMDir /r $INSTDIR
SectionEnd
