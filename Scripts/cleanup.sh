# 设置脚本的错误处理
set -e          # 在出现错误时退出脚本
set -u          # 未定义变量时退出
set -o pipefail # 管道中任一命令失败时退出

# 获取当前脚本所在的目录
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# 定义 ExternalLibs 目录路径
EXTERNAL_LIBS_OUTPUT_DIR="$SCRIPT_DIR/../Libs"

rm -rf $EXTERNAL_LIBS_OUTPUT_DIR
