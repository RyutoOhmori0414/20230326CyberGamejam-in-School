using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CustomPostProcess : ScriptableRendererFeature
{
    [System.Serializable]
    public class CustomPostProcessSettings
    {
        public RenderPassEvent Event = RenderPassEvent.BeforeRenderingPostProcessing;
        public Shader Shader;
    }

    public CustomPostProcessSettings settings;
    public CustomPostProcessPass pass;

    public override void Create()
    {
        this.name = "Custom PostProcess";
        pass = new CustomPostProcessPass(settings.Event, settings.Shader);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        pass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
        renderer.EnqueuePass(pass);
    }
}
