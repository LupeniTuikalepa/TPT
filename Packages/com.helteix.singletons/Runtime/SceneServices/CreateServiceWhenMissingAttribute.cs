using System;

namespace Helteix.Singletons.SceneServices
{
    public class CreateServiceWhenMissingAttribute : Attribute
    {
        public readonly bool create;

        public CreateServiceWhenMissingAttribute(bool create = true)
        {
            this.create = create;
        }
    }
}