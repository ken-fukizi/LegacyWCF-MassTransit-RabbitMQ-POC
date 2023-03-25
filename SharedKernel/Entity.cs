using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public class Entity
    {
        protected Entity()
        {
            
        }
        int? _requestedHashCode;
        int? _id;
        public virtual int Id
        {
            get { return _id ?? 0; }
            protected set { _id = value; }
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Entity item = (Entity)obj;
            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public void ResetAsNew()
        {
            Id = default;
        }

        private List<INotification> _domainEvents;

        public List<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification notification)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(notification);
        }
        public void RemoveDomainEvent(INotification notification)
        {
            if(_domainEvents == null) return;
            _domainEvents.Remove(notification);
        }

        public bool ContainsDomainEvent(INotification notification) {  return _domainEvents.Contains(notification); }
    }
}
