using Popupnik.Server.Components.Model.Entities;

namespace Popupnik.Server.Components.Model
{
    //TODO: Remove static property
    public sealed class EntityFactory
    {
        private static volatile EntityFactory instance;
        private static object syncRoot = new object();

        public static EntityFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EntityFactory();
                        }
                    }
                }
                return instance;
            }
        }

        private EntityFactory()
        {
        }

        public User CreateUser()
        {
            return new User();
        }

        public Message CreateMessage()
        {
            return new Message();
        }
    }
}