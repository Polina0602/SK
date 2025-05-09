using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;


namespace SemanticKernelPlayground.Setup
{
    public class SetupKernel
    {
        public static Kernel CreateKernel(IConfiguration configuration)
        {
            var modelName = configuration["ModelName"] ?? throw new ApplicationException("ModelName not found");
            var endpoint = configuration["Endpoint"] ?? throw new ApplicationException("Endpoint not found");
            var apiKey = configuration["ApiKey"] ?? throw new ApplicationException("ApiKey not found");

            var builder = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(modelName, endpoint, apiKey);

            AddPlugins.Register(builder);
            return builder.Build();
        }
    }
}
