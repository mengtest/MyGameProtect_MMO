rem �л���.protoЭ�����ڵ�Ŀ¼  
cd protobuf
rem ����ǰ�ļ����е�����Э���ļ�ת��Ϊlua�ļ�  
for %%i in (*.proto) do (    
echo %%i
protoc.exe --lua_out=../protolua --plugin=protoc-gen-lua="..\..\Tools\protoc-gen-lua\plugin\protoc-gen-lua.bat" %%i
)  
echo end  
pause 