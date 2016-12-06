using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace NetMonkey
{

    /// <summary>Base class for a query that can include or exclude fields.</summary>
    /// <typeparam name="TModel">The model type that will be returned by the query.</typeparam>
    public class FieldsQuery<TModel>:
        BaseQuery
        where TModel:
            Model.IModelObject
    {

        /// <summary>Creates a new instance of the <see cref="FieldsQuery{TModel}"/> type.</summary>
        public FieldsQuery()
        { }

        /// <summary>Excludes the specified property from the results.</summary>
        /// <typeparam name="TProperty">The type of the property to exclude.</typeparam>
        /// <param name="expression">The expression that specifies the path to the property.</param>
        public FieldsQuery<TModel> ExcludeProperty<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (ExcludedProperties==null)
                ExcludedProperties=new List<LambdaExpression>();
            ExcludedProperties.Add(expression);

            return this;
        }

        /// <summary>Specifically includes the specified property in the results.</summary>
        /// <typeparam name="TProperty">The type of the property to include.</typeparam>
        /// <param name="expression">The expression that specifies the path to the property.</param>
        public FieldsQuery<TModel> IncludeProperty<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (IncludedProperties==null)
                IncludedProperties=new List<LambdaExpression>();
            IncludedProperties.Add(expression);

            return this;
        }

        /// <summary>Gets the query parameters for the current instance.</summary>
        /// <returns>The query parameters for the current instance.</returns>
        protected override NameValueCollection GetQueryParameters()
        {
            var parameters = base.GetQueryParameters();

            if ((IncludedProperties!=null) && IncludedProperties.Any())
            {
                var fields = new StringBuilder();
                foreach (var property in IncludedProperties)
                {
                    var field = new StringBuilder();

                    var member = property.Body as MemberExpression;
                    if (member!=null)
                        _CreateJsonPath(field, member);

                    if (field.Length>0)
                    {
                        if (fields.Length>0)
                            fields.Append(',');
                        fields.Append(field.ToString());
                    }
                }
                if (fields.Length>0)
                    parameters.Add("fields", fields.ToString());
            }

            if ((ExcludedProperties!=null) && ExcludedProperties.Any())
            {
                var fields = new StringBuilder();
                foreach (var property in IncludedProperties)
                {
                    var field = new StringBuilder();

                    var member = property.Body as MemberExpression;
                    if (member!=null)
                        _CreateJsonPath(field, member);

                    if (field.Length>0)
                    {
                        if (fields.Length>0)
                            fields.Append(',');
                        fields.Append(field.ToString());
                    }
                }
                if (fields.Length>0)
                    parameters.Add("exclude_fields", fields.ToString());
            }

            return parameters;
        }

        private void _CreateJsonPath(StringBuilder builder, Expression expression)
        {
            Debug.Assert(builder!=null);
            if (builder==null)
                throw new ArgumentNullException("builder");
            Debug.Assert(expression!=null);
            if (expression==null)
                throw new ArgumentNullException("expression");

            var member = expression as MemberExpression;
            if (member!=null)
            {
                MemberInfo info = member.Member;

                var attribute = info.GetCustomAttribute<JsonPropertyAttribute>(true);
                if (attribute!=null)
                {
                    if (builder.Length>0)
                        builder.Insert(0, '.');
                    builder.Insert(0, attribute.PropertyName);
                } else
                {
                    if (builder.Length>0)
                        builder.Insert(0, '.');
                    builder.Insert(0, info.Name.ToLowerInvariant());
                }

                if (info.DeclaringType!=typeof(TModel))
                    _CreateJsonPath(builder, member.Expression);
                return;
            }

            var methodCall = expression as MethodCallExpression;
            if (methodCall!=null)
            {
                //TODO: only for indexers ?
                _CreateJsonPath(builder, methodCall.Object);
                return;
            }

            var lambda = expression as LambdaExpression;
            if (lambda!=null)
            {
                _CreateJsonPath(builder, lambda.Body);
                return;
            }
        }

        /// <summary>Gets the list of properties to exclude from the results.</summary>
        protected IList<LambdaExpression> ExcludedProperties { get; private set; }

        /// <summary>Gets the list of properties to include in the results.</summary>
        protected IList<LambdaExpression> IncludedProperties { get; private set; }
    }
}
