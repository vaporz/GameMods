1, 创建项目，模板选择“C#类库”，目标框架选择“.NET Standard 2.1”
2，添加依赖Assembly-CSharp和UnityEngine.CoreModule，安装包Newtonsoft.Json
3，创建Plugin脚本，继承MonoBehaviour，
	ScenePlugin: 添加[ScenePlugin("PluginName")]
	PartPlugin: 实现接口IPlugin，添加[PartPlugin("PluginName")]
4，编写插件代码，生成dll文件
5，把dll文件放到mods文件夹内
