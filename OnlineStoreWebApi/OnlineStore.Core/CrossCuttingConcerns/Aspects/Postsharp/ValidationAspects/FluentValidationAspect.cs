using FluentValidation;
using OnlineStore.Core.CrossCuttingConcerns.Validation.FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.ValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private Type _validatorType;

        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(_=>_.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }

        }
    }
}
