using System.Reflection;

namespace KnowledgeHub.Application;

public class AssemblyMarker
{
    public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}