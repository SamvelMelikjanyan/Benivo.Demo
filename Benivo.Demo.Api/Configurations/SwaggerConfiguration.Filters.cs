﻿using Benivo.Demo.Common.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Benivo.Demo.Api.Configurations
{
    internal class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == ApiVersioning.Version);
            operation.Parameters.Remove(versionParameter);
        }
    }

    internal class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            OpenApiPaths openApiPaths = new();

            foreach (var path in swaggerDoc.Paths)
                openApiPaths.Add(path.Key.Replace($"v{ApiVersioning.Version}", swaggerDoc.Info.Version), path.Value);

            swaggerDoc.Paths = openApiPaths;
        }
    }
}
