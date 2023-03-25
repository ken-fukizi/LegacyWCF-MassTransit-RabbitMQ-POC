using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public abstract class Enumeration : Entity, IComparable
    {
        
        private readonly string _displayName;
        protected Enumeration()
        {
        }
        protected Enumeration(int value, string displayName)
        {
            base.Id = value;
            
            _displayName = displayName;
        }
        public int Value
        {
            get { return Id; }
        }
        public string DisplayName
        {
            get { return _displayName; }
        }
        public override string ToString()
        {
            return DisplayName;
        }
        public static IEnumerable<T> GetAll<T>() where T : Enumeration//, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var info in fields)
            {
                //var instance = new T();
                var locatedValue = info.GetValue(null) as T;
                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null)
            {
                return false;
            }
            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Value);
            return typeMatches && valueMatches;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }
        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }
        // Used to check if two enums are equal by their value
        public static bool operator ==(Enumeration a, Enumeration b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }
        public static bool operator !=(Enumeration a, Enumeration b)
        {
            return !(a == b);
        }

        public static ICollection<string> ValidateId<T>(int id) where T : Enumeration
        {
            List<string> errors = new List<string>();
            var exists = GetAll<T>().Any( im => im.Id == id);
            if (exists)
            {
                errors.Add($"{typeof(T).Name} Id [{id}] does not exist");
            }
            return errors;
        }

        public static T FromValue<T>(int value, bool returnNullAndDontThrowIfNotFound = false) where T : Enumeration
        {
            try
            {
                var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
                return matchingItem;
            }
            catch (Exception)
            {
                if (returnNullAndDontThrowIfNotFound) { return null; }
                throw;               
            }          
        }

        public static T FromDisplayName<T>(string displayName, bool returnNullAndDontThrowIfNotFound = false) where T : Enumeration
        {
            try
            {
                var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
                return matchingItem;
            }
            catch (Exception)
            {
                if (returnNullAndDontThrowIfNotFound) { return null; }
                throw;
            }
        }   

        private static T Parse<T, T1>(T1 value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem != null) { return matchingItem; }
            var message = $"Can't find a matching enumeration value in {typeof(T)} where {description} = {value.ToString()}";
            throw new InvalidOperationException(message);
        }
    }   
}
