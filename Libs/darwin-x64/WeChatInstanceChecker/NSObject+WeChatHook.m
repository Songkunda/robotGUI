#import "NSObject+WeChatHook.h"

@implementation NSObject (WeChatHook)

+ (void)KDHookWeChat {
  KDHookClassMethod(objc_getClass("CUtility"), @selector(HasWechatInstance),
                    [self class], @selector(KDHasWechatInstance));
}
#pragma mark - hook 方法
/**
 hook 是否已启动
 */
+ (BOOL)KDHasWechatInstance {
  NSLog(@"kd hook_HasWechatInstance");
  return NO;
}
@end