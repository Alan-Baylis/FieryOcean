//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public interface IPlayerView {

    PlayerViewComponent playerView { get; }
    bool hasPlayerView { get; }

    void AddPlayerView(IPlayerController newController);
    void ReplacePlayerView(IPlayerController newController);
    void RemovePlayerView();
}
