#!/bin/bash

# 设置脚本的错误处理
set -e          # 在出现错误时退出脚本
set -u          # 未定义变量时退出
set -o pipefail # 管道中任一命令失败时退出

# 获取当前脚本所在的目录
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# 定义 ExternalLibs 目录路径
EXTERNAL_LIBS_DIR="$SCRIPT_DIR/../ExternalLibs"
TOOLS_DIR="$SCRIPT_DIR/../Tools"
EXTERNAL_LIBS_OUTPUT_DIR="$SCRIPT_DIR/../Libs"
DYLIB_WECHAT_INSTANCE=$EXTERNAL_LIBS_OUTPUT_DIR/darwin-x64/libWechatInstance.dylib
WECHAT_APP_PATH="/Applications/WeChat.app/Contents/MacOS/WeChat"
WECHAT_APP_BACKUP_PATH="/Applications/WeChat.app/Contents/MacOS/WeChat.bak"

# 检查 $WECHAT_APP_BACKUP_PATH 目录是否存在
if [ ! -d "$WECHAT_APP_BACKUP_PATH"]; then
    sudo cp -v $WECHAT_APP_PATH $WECHAT_APP_BACKUP_PATH
else
    sudo cp -v $WECHAT_APP_BACKUP_PATH $WECHAT_APP_PATH
fi

# sudo cp -v ./Libs/darwin-x64/libWechatInstance.dylib /Applications/WeChat.app/Contents/MacOS/
# sudo ./Tools/darwin-x64/optool14 install -c load -p "/Applications/WeChat.app/Contents/MacOS/libWechatInstance.dylib" -t /Applications/WeChat.app/Contents/MacOS/WeChat
# DYLD_INSERT_LIBRARIES=/Applications/WeChat.app/Contents/MacOS/libWechatInstance.dylib /Applications/WeChat.app/Contents/MacOS/WeChat
# 注入
sudo $TOOLS_DIR/insert_dylib $DYLIB_WECHAT_INSTANCE $WECHAT_APP_PATH --all-yes --overwrite
# sudo chown -R $(whoami) /Applications/WeChat.app
# sudo cp -v /Applications/WeChat.app/Contents/MacOS/WeChat_patched /Applications/WeChat.app/Contents/MacOS/WeChat

# 查看注入是否成功
otool -L /Applications/WeChat.app/Contents/MacOS/WeChat | grep Wechat
