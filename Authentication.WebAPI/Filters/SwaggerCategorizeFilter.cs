using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading;

namespace Authentication.WebAPI.Filters
{
    public class SwaggerCategorizeFilter :IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IEnumerable<string> segments = GetSegments(context);

            string segment = string.Join(" - ", segments);

            if (segment != context.ApiDescription.GroupName) operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = segment } };
        }

        private IEnumerable<string> GetSegments(OperationFilterContext context)
        {
            if (context?.ApiDescription?.RelativePath == null)
                yield break;

            string[] segments = context.ApiDescription.RelativePath.Split("/");

            for (var i = 1; i < segments.Length - 1; i++)
                yield return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(segments[i].Replace("-", " "));
        }
    }
}
