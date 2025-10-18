using System.Reflection;

namespace KnowledgeHub.Infrastructure;

public static class AssemblyMarker
{
    public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}