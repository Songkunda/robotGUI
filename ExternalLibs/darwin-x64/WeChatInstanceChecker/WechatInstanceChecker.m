// WechatInstanceChecker.m
// codesign --sign - --force --deep /Applications/WeChat.app
#import <Foundation/Foundation.h>
#import <objc/message.h>
#import <objc/runtime.h>

// 引入dylib 自动加载函数

@implementation NSObject (MultipleInstances)

static void __attribute__((constructor)) instace(void) {
  NSLog(@"instace");
  Class class = object_getClass(NSClassFromString(@"CUtility"));
  SEL selector = NSSelectorFromString(@"HasWechatInstance");
  Method method = class_getInstanceMethod(class, selector);
  IMP imp = imp_implementationWithBlock(^(id self) {
    NSLog(@"imp_implementationWithBlock");
    return 0; // 永远返回0
  });
  class_replaceMethod(class, selector, imp, method_getTypeEncoding(method));
}
@end