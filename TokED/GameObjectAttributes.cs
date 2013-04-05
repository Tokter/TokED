using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED
{
    public interface IRequiresParentMetadata
    {
        string IsRequiringParent { get; set; }
    }

    public interface IAllowsChildMetadata
    {
        string IsAllowingChild { get; set; }
    }

    public interface IDoesNotAllowChildMetadata
    {
        string IsNotAllowingChild { get; set; }
    }

    public interface IDoesNotAllowChildrenMetadata
    {
        bool IsNotAllowingChildred { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class RequiresParent : ExportAttribute, IRequiresParentMetadata
    {
        public RequiresParent(string parent)
        {
            IsRequiringParent = parent;
        }

        public string IsRequiringParent { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class AllowsChild : ExportAttribute, IAllowsChildMetadata
    {
        public AllowsChild(string child)
        {
            IsAllowingChild = child;
        }

        public string IsAllowingChild { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class DoesNotAllowChild : ExportAttribute, IDoesNotAllowChildMetadata
    {
        public DoesNotAllowChild(string child)
        {
            IsNotAllowingChild = child;
        }

        public string IsNotAllowingChild { get; set; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DoesNotAllowChildren : ExportAttribute, IDoesNotAllowChildrenMetadata
    {
        public DoesNotAllowChildren()
        {
            IsNotAllowingChildred = true;
        }

        public bool IsNotAllowingChildred { get; set; }
    }
}
