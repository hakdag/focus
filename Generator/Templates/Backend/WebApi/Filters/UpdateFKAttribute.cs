using BetonCRM.Common.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System;
using BetonCRM.Common.Attributes;

namespace BetonCRM.Filters
{
    public class UpdateFKAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Count == 0) return;

            var model = actionContext.ActionArguments.ElementAt(0).Value;
            var modelType = actionContext.ActionArguments.ElementAt(0).Value.GetType();
            var actionName = actionContext.ActionDescriptor.ActionName;

            if (modelType.BaseType != typeof(BaseModel)) return;
            if (actionName != "Post" && actionName != "Put") return;

            updateFK(model as BaseModel, modelType, null);
        }

        private static void updateFK(BaseModel model, System.Type modelType, object baseModel)
        {
            var virtualProperties = modelType.GetProperties().Where(p => p.GetAccessors().Any(a => a.IsVirtual));
            foreach (PropertyInfo pi in virtualProperties)
            {
                if (baseModel != null && baseModel.GetType() == pi.PropertyType) continue;

                // collection ile gelen modelleri de güncelle
                if (((TypeInfo)pi.PropertyType).ImplementedInterfaces.Contains(typeof(IEnumerable)))
                {
                    var collection = (IEnumerable)pi.GetValue(model);
                    if (collection == null)
                        continue;

                    foreach (BaseModel subModel in collection)
                    {
                        updateFK(subModel, subModel.GetType(), model);
                    }
                    continue;
                }

                var fkAttr = pi.CustomAttributes.FirstOrDefault(ca => ca.AttributeType == typeof(ForeignKeyAttribute));
                if (fkAttr == null) continue;

                var fkPropertyName = fkAttr.ConstructorArguments[0].Value.ToString();
                var fkProperty = modelType.GetProperties().FirstOrDefault(p => p.Name == fkPropertyName);
                if (fkProperty == null) continue;

                var idProperty = pi.PropertyType.GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(ca => ca.AttributeType == typeof(KeyAttribute)));
                if (idProperty == null) continue;

                var piValue = pi.GetValue(model) as BaseModel;
                if (piValue == null) continue;

                if (!hasPassModel(pi) || (hasPassModel(pi) && !piValue.InsertNew)) // eğer true ise, veri çoklamasını engeller. False ise, insert yapar.
                {
                    fkProperty.SetValue(model, idProperty.GetValue(piValue)); // foreign key'in bağlı olduğu property ataması yapılıyor.
                    pi.SetValue(model, null); // Foreign key'in bağlı olduğu virtual property null olarak atanıyor. Eğer bu property'de değer olursa veri çokluyor.
                }
                else
                    updateFK(piValue, pi.PropertyType, model);
            }
        }

        private static bool hasPassModel(PropertyInfo pi)
        {
            return pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(PassModelAttribute));
        }
    }
}