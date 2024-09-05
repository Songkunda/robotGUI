#!/bin/bash
if [ `uname` == "Darwin" ]; then
    gcc -dynamiclib -o Libs/darwin-x64/libWechatInstance.dylib Libs/darwin-x64/WeChatInstanceChecker/WechatInstanceChecker.m -framework Foundation -v 
fi

