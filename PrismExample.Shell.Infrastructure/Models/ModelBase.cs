using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrismExample.Shell.Infrastructure.Models
{
    public abstract class ModelBase<TDto> : DynamicObject, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IDictionary<string, IList<string>> changeCascadePropertyList = new ConcurrentDictionary<string, IList<string>>();
        private bool isDeleted;

        protected readonly TDto Dto;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:VZ.CiresPlus.Model.Model.ModelBase`1" /> class.
        /// </summary>
        /// <param name="data">The dto data.</param>
        protected ModelBase(TDto data)
        {
            Dto = data;
        }

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The error.
        /// </summary>
        public string Error => string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this View Model was edited or not.
        /// </summary>
        /// <value>True if edited.</value>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this View Model was deleted or not.
        /// </summary>
        /// <value>True if deleted.</value>
        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                if (isDeleted != value)
                {
                    isDeleted = value;
                    IsModified = true;
                    RaisePropertyChanged();
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.String" />.
        /// </returns>
        public string this[string columnName]
        {
            get
            {
                string result = null;

                IEnumerable<ValidationResult> data = Validate(new ValidationContext(this, null, null));

                switch (columnName)
                {
                    case "":
                        result += string.Join(Environment.NewLine, data.Select(x => x.ErrorMessage));
                        break;

                    default:
                        result += string.Join(Environment.NewLine, data.Where(x => x.MemberNames.Contains(columnName)).Select(x => x.ErrorMessage));
                        break;
                }

                return result;
            }
        }

        public TDto GetDto()
        {
            return Dto;
        }

        /// <inheritdoc />
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return TryGetMember(binder.Name, out result);
        }

        /// <inheritdoc />
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return TrySetMember(binder.Name, value);
        }

        /// <summary>
        /// Notifies the model about changes within the underlying dto property.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        public virtual void DataPropertyChanged(string propertyName)
        {
        }

        /// <summary>
        /// Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="result">The object to set the result in.</param>
        /// <returns>True if value could be retrieved.</returns>
        public bool TryGetMember(string propertyName, out object result)
        {
            result = null;

            if (Dto == null)
            {
                ////this.tracer.Debug("Tried to get property on null object.");
                return false;
            }

            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(Dto)[propertyName];

            if (descriptor == null)
            {
                ////this.tracer.DebugFormat("Couldn't find property with Name \"{0}\".", propertyName);
                return false;
            }

            try
            {
                result = descriptor.GetValue(Dto);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the specified value for the provided property.
        /// </summary>
        /// <param name="propName">The property name.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if value could be set.</returns>
        public bool TrySetMember(string propName, object value)
        {
            string propertyName = propName;

            if (Dto == null)
            {
                ////this.tracer.Debug("Tried to get property on null object.");
                return false;
            }

            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(Dto)[propertyName];

            if (descriptor == null)
            {
                ////this.tracer.DebugFormat("Couldn't find property with Name \"{0}\".", propertyName);
                return false;
            }

            try
            {
                if (value == null && (descriptor.PropertyType.IsValueType || descriptor.PropertyType.IsPrimitive))
                {
                    value = Activator.CreateInstance(descriptor.PropertyType);
                }

                if (descriptor.Converter != null && (value != null && descriptor.Converter.CanConvertFrom(value.GetType())))
                {
                    value = descriptor.Converter.ConvertFrom(value);
                }

                // type savety: for example decimal cannot be converted implicitly to nullable double
                Type type = Nullable.GetUnderlyingType(descriptor.PropertyType) ?? descriptor.PropertyType;
                object newValue = (value == null) ? null : Convert.ChangeType(value, type);
                object oldValue = descriptor.GetValue(Dto);

                if ((newValue == null && oldValue == null) || (newValue != null && newValue.Equals(oldValue)))
                {
                    return true;
                }

                descriptor.SetValue(Dto, newValue);

                IsModified = true;
                RaisePropertyChanged(nameof(IsModified));
                RaisePropertyChanged(propName);
                DataPropertyChanged(propName);
                NotifyCascadingProperties(propertyName);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Raises the property changed handler.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="reevaluate">Reevaluate properties</param>
        public virtual void RaisePropertyChanged(string propertyName = null, bool reevaluate = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));            
        }

        /// <summary>
        /// Raises property changed events for all properties of this instance.
        /// </summary>
        public void RaiseAllPropertiesChanged()
        {
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                RaisePropertyChanged(property.Name, false);
            }

            RaiseDataPropertiesChanged();
        }

        /// <summary>
        /// Raises property changed events for all properties of the data instance.
        /// </summary>
        public void RaiseDataPropertiesChanged()
        {
            PropertyInfo[] properties = Dto.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                RaisePropertyChanged(property.Name);
            }
        }

        #region Validation Helper

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this, null, null), true);
        }

        public void Delete()
        {
            IsDeleted = true;
            RaisePropertyChanged("IsDeleted");
        }

        #endregion

        #region IDataErrorInfo

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        #endregion

        /// <summary>
        /// Extracts the name of a property from an expression.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression returning the property's name.</param>
        /// <returns>The name of the property returned by the expression.</returns>
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(@"Invalid argument", nameof(propertyExpression));
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException(@"Argument is not a property", nameof(propertyExpression));
            }

            return propertyInfo.Name;
        }

        protected void RegisterChangeCascadeProperties(string changeSourcePropertyName, string[] targetNotifyProperties)
        {
            changeCascadePropertyList.Add(new KeyValuePair<string, IList<string>>(changeSourcePropertyName, targetNotifyProperties));
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property that changed.</param>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChangedEventHandler changedEventHandler = PropertyChanged;
            if (changedEventHandler == null)
            {
                return;
            }

            string propertyName = GetPropertyName(propertyExpression);
            changedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Private Helper

        private void NotifyCascadingProperties(string changedPropertyName)
        {
            IList<string> propertyNamesToNotify;
            if (changeCascadePropertyList.TryGetValue(changedPropertyName, out propertyNamesToNotify))
            {
                if (propertyNamesToNotify != null)
                {
                    foreach (var propertyName in propertyNamesToNotify)
                    {
                        RaisePropertyChanged(propertyName);
                    }
                }
            }
        }

        #endregion
    }
}
