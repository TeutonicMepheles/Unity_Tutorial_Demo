# Unity_Tutorial_Demo
持续学习Unity中……

### 221127 物体控制与摄像机运动控制
- 学习物体的实例化与销毁，物体的运动调节，贴图材质设置
- 摄像机和光标跟随物体的方式，摄像机视角随光标旋转变化的方式（目前有一些小bug没修好）

### 221128 对象池应 & 线渲染和拖尾渲染
- 创建对象池的三种方法：简单的使用脚本实例化并销毁/Unity2021版本后内置的对象池API/使用单例自己创建对象池
- 单例/线程管理/泛型
- 目前的BUG：自己创建单例的时候没有办法使用Single<>（221128已解决，因为脚本文件夹中缺少Single文件，Single类不在MonoBehavior中，而是需要自己编写）
- 尝试了基础的线渲染和拖尾渲染，但目前因为对脚本逻辑不够熟悉所以感觉无法深入往下，决定转变策略先开始回归C#基础

### 221129 《Unity和C#游戏编程入门》
- 复习了C#代码基础，主要需要梳理Unity当中对象-组件的关系以及获取权限的关系

### 221202 《Unity和C#游戏编程入门》
- 角色移动、摄像机及碰撞
- 游戏机制脚本编写：跳跃、发射子弹、游戏管理器、基础GUI
