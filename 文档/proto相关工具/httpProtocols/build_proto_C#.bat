rem �л���.protoЭ�����ڵ�Ŀ¼  
cd protobuf
rem ����ǰ�ļ����е�����Э���ļ�ת��Ϊlua�ļ�  
for %%i in (*.proto) do (    
echo %%i  
start ../../Tools/ProtoGen/protogen.exe  -i:%%i  -o:../Protocols/%%~niProtocols.cs -p:lightFramework=true
  
)  
echo end  
pause 