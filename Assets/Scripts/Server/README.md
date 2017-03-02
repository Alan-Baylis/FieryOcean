请注意，如果KBEngine插件使用的是多线程模式，kbe_scripts默认在子线程处理与插件处于同一线程，请不要和u3d_scripts直接交互，应该通过KBE提供的事件机制交互。

这个文件夹(kbe_scripts)中的脚本类似于KBEngine资产库中（https://github.com/kbengine/kbengine_demos_assets）scripts/client文件夹的Python逻辑脚本。
KBEngine的理想环境是都使用Python实现游戏逻辑，https://github.com/kbengine/kbengine_demos_assets中scripts之下存在client、base、cell三个文件夹分别对应client（客户端）、baseapp（服务器）、cellapp（服务器)进程的逻辑脚本。
https://github.com/kbengine/kbengine_ogre_demo例子演示了KBE的理想环境，客户端插件(kbengine.dll封装的client_lib)由C++编写，自带了Python解释器与entitydef的分析模块还有网络与消息协议处理等等，
由C++客户端插件驱动来scripts/client中的脚本逻辑。

但这种理想环境在Unity3d中无法很好的实现，因此实现了轻量级的Unity3d专用插件(协议与entitydef由服务端网络导入)，在Unity3d中使用主流的C#来完成客户端部分的脚本逻辑，
此时KBEngine资产库可以不需要client文件夹。如果没有scripts/client文件夹需要注意一些事项http://www.kbengine.org/cn/docs/configuration/entities.html。
