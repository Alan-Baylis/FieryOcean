using System;
using Entitas.VisualDebugging.Unity.Editor;

public class DefaultIViewControllerInstanceCreator : IDefaultInstanceCreator {

    public bool HandlesType(Type type) {
        return type == typeof(IViewController);
    }

    public object CreateDefault(Type type) {
        // TODO return an instance of type IViewController
        throw new NotImplementedException();
    }
}
