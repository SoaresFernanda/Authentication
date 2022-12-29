using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Authentication.WebAPI.Filters
{
    public class SwaggerOrderOperationFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            IList<KeyValuePair<string, OpenApiPathItem>> paths = swaggerDoc.Paths
                .OrderBy(pair => pair.Key)
                .Select(pair => new KeyValuePair<string, OpenApiPathItem>(pair.Key, pair.Value))
                .ToList();

            swaggerDoc.Paths.Clear();

            foreach (KeyValuePair<string, OpenApiPathItem> pair in paths) swaggerDoc.Paths.Add(pair.Key, pair.Value);
        }
    }
}
