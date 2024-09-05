#import <Foundation/Foundation.h>
#import <objc/runtime.h>

@interface KNHook : NSObject
/**
 替换对象方法

 @param originalClass 原始类
 @param originalSelector 原始类的方法
 @param swizzledClass 替换类
 @param swizzledSelector 替换类的方法
 */
+ (BOOL)KDHookMethod:(Class)originalClass
    originalSelector:(SEL)originalSelector
       swizzledClass:(Class)swizzledClass
    swizzledSelector:(SEL)swizzledSelector {
  printf("libWechatInstance.dylib kdHookMethod \n");

  Method originalMethod =
      class_getInstanceMethod(originalClass, originalSelector);
  if (originalMethod) {
    printf("libWechatInstance.dylib kdHookMethod originalMethod Success\n");
  } else {
    printf("libWechatInstance.dylib kdHookMethod originalMethod Failed\n");
  }

  Method swizzledMethod =
      class_getInstanceMethod(swizzledClass, swizzledSelector);
  if (swizzledMethod) {
    printf("libWechatInstance.dylib kdHookMethod swizzledMethod Success\n");
  } else {
    printf("libWechatInstance.dylib kdHookMethod swizzledMethod Failed\n");
  }

  if (originalMethod && swizzledMethod) {
    method_exchangeImplementations(originalMethod, swizzledMethod);
    printf("libWechatInstance.dylib kdHookMethod Done\n");
    return YES;
  }

  printf("libWechatInstance.dylib kdHookMethod Failed\n");
  return NO;
}

/**
 替换类方法

 @param originalClass 原始类
 @param originalSelector 原始类的类方法
 @param swizzledClass 替换类
 @param swizzledSelector 替换类的类方法
 */
+ (BOOL)KDHookClassMethod:(Class)originalClass
         originalSelector:(SEL)originalSelector
            swizzledClass:(Class)swizzledClass
         swizzledSelector:(SEL)swizzledSelector {
  printf("libWechatInstance.dylib kdHookClassMethod \n");

  Method originalMethod = class_getClassMethod(originalClass, originalSelector);
  if (originalMethod) {
    printf(
        "libWechatInstance.dylib kdHookClassMethod originalMethod Success\n");
  } else {
    printf("libWechatInstance.dylib kdHookClassMethod originalMethod Failed\n");
  }

  Method swizzledMethod = class_getClassMethod(swizzledClass, swizzledSelector);
  if (swizzledMethod) {
    printf(
        "libWechatInstance.dylib kdHookClassMethod swizzledMethod Success\n");
  } else {
    printf("libWechatInstance.dylib kdHookClassMethod swizzledMethod Failed\n");
  }

  if (originalMethod && swizzledMethod) {
    method_exchangeImplementations(originalMethod, swizzledMethod);
    printf("libWechatInstance.dylib kdHookClassMethod Done\n");
    return YES;
  }

  printf("libWechatInstance.dylib kdHookClassMethod Failed\n");
  return NO;
}

/**
 打印实例方法
 */
+ (void)printInstanceMethods:(Class)cls {
  if (cls == NULL) {
    printf("Class is NULL\n");
    return;
  }

  unsigned int methodCount = 0;
  Method *methods = class_copyMethodList(cls, &methodCount);

  if (methodCount == 0) {
    printf("No instance methods found for class %s\n", class_getName(cls));
  } else {
    printf("Instance methods for class %s:\n", class_getName(cls));
    for (unsigned int i = 0; i < methodCount; i++) {
      SEL methodName = method_getName(methods[i]);
      printf(" - %s\n", sel_getName(methodName));
    }
  }
  free(methods);
}

/**
 打印类方法
 */
+ (void)printClassMethods:(Class)cls {
  Class metaClass = object_getClass((id)cls); // 获取元类 (meta-class)

  unsigned int methodCount = 0;
  Method *methods = class_copyMethodList(metaClass, &methodCount);

  printf("Class methods for class %s:\n", class_getName(cls));
  for (unsigned int i = 0; i < methodCount; i++) {
    SEL methodName = method_getName(methods[i]);
    printf(" + %s\n", sel_getName(methodName));
  }

  free(methods);
}

/**
 打印所有类
 */
+ (void)printAllClasses {
  int numClasses = objc_getClassList(NULL, 0);
  Class *classes =
      (__unsafe_unretained Class *)malloc(sizeof(Class) * numClasses);
  numClasses = objc_getClassList(classes, numClasses);

  printf("All classes in runtime:\n");
  for (int i = 0; i < numClasses; i++) {
    printf("Class #%d: %s\n", i, class_getName(classes[i]));
  }

  free(classes);
}

@end
