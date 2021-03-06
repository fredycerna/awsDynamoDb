using System.Runtime.InteropServices;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace awsDynamoDbPoC.Tests.Setup;

public class TestContext : IAsyncLifetime
{
    private readonly DockerClient _dockerClient;
    private const string ContainerImageUri = "amazon/dynamodb-local";
    private string _containerId = string.Empty;
    private readonly Uri _defaultWindowsDockerEngineUri = new Uri("npipe://./pipe/docker_engine");
    private readonly Uri _defaultLinuxDockerEngineUri = new Uri("unix:///var/run/docker.sock");

    public TestContext()
    {
        _dockerClient = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 
            new DockerClientConfiguration(_defaultWindowsDockerEngineUri).CreateClient() : 
            new DockerClientConfiguration(_defaultLinuxDockerEngineUri).CreateClient();
    }
    
    public async Task InitializeAsync()
    {
        await PullImage();
        await StartContainer();
        await new TestDataSetup().CreateTable();
    }

    public async Task DisposeAsync()
    {
        if (!string.IsNullOrEmpty(_containerId))
        {
            await _dockerClient.Containers.KillContainerAsync(_containerId, new ContainerKillParameters());
        }
    }

    private async Task PullImage()
    {
        await _dockerClient.Images.CreateImageAsync(new ImagesCreateParameters
        {
         FromImage   = ContainerImageUri,
         Tag = "latest"
        }, 
            new AuthConfig(),
        new Progress<JSONMessage>());
    }

    private async Task StartContainer()
    {
        var response = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = ContainerImageUri, 
            ExposedPorts = new Dictionary<string, EmptyStruct>
            {
                {
                    "8000", default(EmptyStruct)
                }   
            },
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {"8000", new List<PortBinding> {new PortBinding {HostPort = "8000"}}}
                },
                PublishAllPorts = true
            }
        });
        _containerId = response.ID;
        await _dockerClient.Containers.StartContainerAsync(_containerId, null);
    }
    
}