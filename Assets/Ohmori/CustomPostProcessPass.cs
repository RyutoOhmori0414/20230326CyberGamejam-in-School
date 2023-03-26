using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPostProcessPass : ScriptableRenderPass
{
    const string k_RenderCustomPostProcessingTag = "Render Custom PostProcessing Effects";
    private RenderTargetIdentifier passSource;
    private RenderTargetHandle passDestination;

    private Material material;
    RenderTargetHandle _TemporaryColorTexture;

    public CustomPostProcessPass(RenderPassEvent renderPassEvent, Shader shader)
    {
        this.renderPassEvent = renderPassEvent;
        if (shader)
        {
            material = new Material(shader);
        }

        _TemporaryColorTexture.Init("_TemporaryColorTexture");
    }

    public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
    {
        this.passSource = source;
        this.passDestination = destination;
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
        opaqueDesc.depthBufferBits = 0;

        var cmd = CommandBufferPool.Get(k_RenderCustomPostProcessingTag);

        Render(cmd, ref renderingData, opaqueDesc);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    void Render(CommandBuffer cmd, ref RenderingData renderingData, RenderTextureDescriptor opaqueDesc)
    {
        cmd.GetTemporaryRT(_TemporaryColorTexture.id, opaqueDesc, FilterMode.Bilinear);

        DoShaderEffect(cmd, passSource, _TemporaryColorTexture, opaqueDesc);

        if (passDestination == RenderTargetHandle.CameraTarget)
        {
            Blit(cmd, _TemporaryColorTexture.Identifier(), passSource);
        }
        else
        {
            Blit(cmd, _TemporaryColorTexture.Identifier(), passDestination.Identifier());
        }
    }

    private void DoShaderEffect(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetHandle destination, RenderTextureDescriptor opaqueDesc)
    {
        Blit(cmd, source, destination.Identifier(), material, 0);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        if (passDestination == RenderTargetHandle.CameraTarget)
        {
            cmd.ReleaseTemporaryRT(_TemporaryColorTexture.id);
        }
    }
}
