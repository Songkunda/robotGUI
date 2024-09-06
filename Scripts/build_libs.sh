#!/bin/bash

# 设置脚本的错误处理
set -e          # 在出现错误时退出脚本
set -u          # 未定义变量时退出
set -o pipefail # 管道中任一命令失败时退出

# 获取当前脚本所在的目录
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# 定义 ExternalLibs 目录路径
EXTERNAL_LIBS_DIR="$SCRIPT_DIR/../ExternalLibs"
EXTERNAL_LIBS_OUTPUT_DIR="$SCRIPT_DIR/../Libs"

# 检查 ExternalLibs 目录是否存在
if [ ! -d "$EXTERNAL_LIBS_DIR" ]; then
    echo "错误: 目录 $EXTERNAL_LIBS_DIR 不存在。"
    exit 1
fi

# 检查 Libs/darwin-x64 目录是否存在
if [ ! -d "$EXTERNAL_LIBS_OUTPUT_DIR/darwin-x64" ]; then
    echo "警告: 目录 $EXTERNAL_LIBS_DIR 不存在。需要创建"
    mkdir -p $EXTERNAL_LIBS_OUTPUT_DIR/darwin-x64
    # exit 1
fi

# 查找所有 .csproj 文件
CSPROJ_FILES=$(find "$EXTERNAL_LIBS_DIR" -type f -name "*.csproj")

# 检查是否找到任何 .csproj 文件
if [ -z "$CSPROJ_FILES" ]; then
    echo "警告: 在 $EXTERNAL_LIBS_DIR 目录下未找到任何 .csproj 文件。"
    # exit 0
fi

# 遍历并编译每个 .csproj 文件
for csproj in $CSPROJ_FILES; do
    echo "正在编译项目: $csproj"
    dotnet build "$csproj" --configuration Release
    echo "项目编译成功: $csproj"
done

if [ $(uname) == "Darwin" ]; then
    gcc -dynamiclib -o $EXTERNAL_LIBS_OUTPUT_DIR/darwin-x64/libWechatInstance.dylib $EXTERNAL_LIBS_DIR/darwin-x64/WeChatInstanceChecker/WechatInstanceChecker.m -framework Foundation -v
fi

echo "所有 ExternalLibs 项目已成功编译。"
