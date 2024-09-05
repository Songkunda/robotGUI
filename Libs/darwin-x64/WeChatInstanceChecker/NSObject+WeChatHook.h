#import <Foundation/Foundation.h>
#import <objc/message.h>
#import <objc/runtime.h>
NS_ASSUME_NONNULL_BEGIN
@interface NSObject (WeChatHook)
+ (void)KDHookWeChat;
+ (BOOL)KDHasWechatInstance;
@end
NS_ASSUME_NONNULL_END