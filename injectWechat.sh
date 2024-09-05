
if [ -f /Applications/WeChat.app/Contents/MacOS/WeChat_bk ]; then
    sudo cp -v /Applications/WeChat.app/Contents/MacOS/WeChat_bk /Applications/WeChat.app/Contents/MacOS/WeChat
else
    sudo cp -v /Applications/WeChat.app/Contents/MacOS/WeChat /Applications/WeChat.app/Contents/MacOS/WeChat_bk
fi

# sudo cp -v ./Libs/darwin-x64/libWechatInstance.dylib /Applications/WeChat.app/Contents/MacOS/
# sudo ./Tools/darwin-x64/optool14 install -c load -p "/Applications/WeChat.app/Contents/MacOS/libWechatInstance.dylib" -t /Applications/WeChat.app/Contents/MacOS/WeChat
# DYLD_INSERT_LIBRARIES=/Applications/WeChat.app/Contents/MacOS/libWechatInstance.dylib /Applications/WeChat.app/Contents/MacOS/WeChat
sudo ./Tools/darwin-x64/insert_dylib /Users/songkunda/Documents/github/wxRobotApp/Libs/darwin-x64/libWechatInstance.dylib /Applications/WeChat.app/Contents/MacOS/WeChat --all-yes --overwrite
# sudo chown -R $(whoami) /Applications/WeChat.app
sudo cp -v /Applications/WeChat.app/Contents/MacOS/WeChat_patched /Applications/WeChat.app/Contents/MacOS/WeChat
otool -L /Applications/WeChat.app/Contents/MacOS/WeChat |grep Wechat