# 飞龙·网页及贴吧操作II #

## 1. WizardHTTP.vb ##

包含WizardHTTP类 继承自System.Net.WebClient 新增加了设置超时和是否重定向的功能

## 2. Utility.vb ##

一些与贴吧操作无关的功能性函数

## 3. TBOps.vb TBOps_TGTMIV.vb ##

基于WizardHTTP和Utility 用于贴吧操作 所有返回信息的处理都要用到LitJson这个开源项目 请到OpenSource官网下载或从我发布的机器中获取

## 4. Result.vb ##

包含Result结构 用于包装返回信息

## 5. BSForm ##

一个验证码队列及窗口的实例

## 6. CHILogin CHIQuickLogin ##

登录苍海国际论坛的实例 包含其窗口 可以修改url用于进行任何论坛的验证

**源码是vb.net写的 用c#的开发者请编译成dll再使用**