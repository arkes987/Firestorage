using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Firestorage.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firestorage.Database
{
    public class Query
    {
        private FirebaseConnector _connection;
        public Query(FirebaseConnector connection)
        {
            _connection = connection;
        }

        public async void Add<T>(T entity)
        {
            var jsonDump = JsonConvert.SerializeObject(entity);
            await _connection.Instance.Child(entity.GetType().Name).PostAsync(jsonDump);
        }
        public async void Update<T>(string key, T entity)
        {
            await _connection.Instance.Child(entity.GetType().Name).Child(key).PutAsync(entity);
        }
        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetById<T>(string id)
        {
            var result = await _connection.Instance.Child(typeof(T).Name).OrderBy("OwnerUserId").EqualTo(id).OnceAsync<T>();
            return result;
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetByEmail<T>(string email)
        {
            var result = await _connection.Instance.Child(typeof(T).Name).OrderBy("Email").EqualTo(email).OnceAsync<T>();
            return result;
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetAccountsByIdentifier<T>(string ident)
        {
            var result = await _connection.Instance.Child(typeof(T).Name).OrderByKey().EqualTo(ident).OnceAsync<T>();
            return result;
        }

        public void ObserveCollection<T>(Action<FirebaseEvent<T>> callback, string userOwnerKey, AccountTypeView accountType)
        {         
            if(accountType == AccountTypeView.All)
            {
                var result = _connection.Instance.Child(typeof(T).Name).OrderBy("OwnerUserId").EqualTo(userOwnerKey).AsObservable<T>().Subscribe(obj => callback(obj));
            }
            else
            {
                var accType = (int)accountType;
                var result = _connection.Instance.Child(typeof(T).Name).OrderBy("OwnerUserId").EqualTo(userOwnerKey).EqualTo(accType).AsObservable<T>().Subscribe(obj => callback(obj));
            }     
        }


        public async void DeleteByKey<T>(string key)
        {
            await _connection.Instance.Child(typeof(T).Name).Child(key).DeleteAsync();
        }
    }
}
